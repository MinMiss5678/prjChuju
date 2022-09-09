using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prjChuju.Models
{
    public partial class dbChujuContext : DbContext
    {
        public dbChujuContext()
        {
        }

        public dbChujuContext(DbContextOptions<dbChujuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountInfo> AccountInfos { get; set; } = null!;
        public virtual DbSet<AccountPicture> AccountPictures { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ActivityImage> ActivityImages { get; set; } = null!;
        public virtual DbSet<ArticleClass> ArticleClasses { get; set; } = null!;
        public virtual DbSet<ArticleContent> ArticleContents { get; set; } = null!;
        public virtual DbSet<ArticleOutline> ArticleOutlines { get; set; } = null!;
        public virtual DbSet<ArticlePicture> ArticlePictures { get; set; } = null!;
        public virtual DbSet<ArticleTagList> ArticleTagLists { get; set; } = null!;
        public virtual DbSet<BookedCase> BookedCases { get; set; } = null!;
        public virtual DbSet<Buildingdb> Buildingdbs { get; set; } = null!;
        public virtual DbSet<Buildingdetail> Buildingdetails { get; set; } = null!;
        public virtual DbSet<CityList> CityLists { get; set; } = null!;
        public virtual DbSet<ClientConnection> ClientConnections { get; set; } = null!;
        public virtual DbSet<CollectArticle> CollectArticles { get; set; } = null!;
        public virtual DbSet<CollectBuilding> CollectBuildings { get; set; } = null!;
        public virtual DbSet<ForgetCode> ForgetCodes { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<RegionList> RegionLists { get; set; } = null!;
        public virtual DbSet<ServiceConnection> ServiceConnections { get; set; } = null!;
        public virtual DbSet<ViewedArticle> ViewedArticles { get; set; } = null!;
        public virtual DbSet<ViewedBuilding> ViewedBuildings { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbChuju;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Role).HasColumnName("role");
            });

            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.ToTable("accountInfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Cellphone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cellphone");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .HasColumnName("gender");

                entity.Property(e => e.Permission)
                    .HasMaxLength(30)
                    .HasColumnName("permission");

                entity.Property(e => e.Region).HasColumnName("region");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasColumnName("userName");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(30)
                    .HasColumnName("userPassword");

                entity.HasOne(d => d.AvatarNavigation)
                    .WithMany(p => p.AccountInfos)
                    .HasForeignKey(d => d.Avatar)
                    .HasConstraintName("FK__accountIn__avata__6754599E");

                entity.HasOne(d => d.RegionNavigation)
                    .WithMany(p => p.AccountInfos)
                    .HasForeignKey(d => d.Region)
                    .HasConstraintName("FK__accountIn__regio__66603565");
            });

            modelBuilder.Entity<AccountPicture>(entity =>
            {
                entity.ToTable("accountPicture");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(255)
                    .HasColumnName("pictureURL");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("uploadDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Uploader).HasColumnName("uploader");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("date")
                    .HasColumnName("modifiedDate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<ActivityImage>(entity =>
            {
                entity.ToTable("ActivityImage");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityImages)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityImage_ActivityOutline");
            });

            modelBuilder.Entity<ArticleClass>(entity =>
            {
                entity.ToTable("articleClass");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(20)
                    .HasColumnName("className");

                entity.Property(e => e.MainClass)
                    .HasMaxLength(20)
                    .HasColumnName("mainClass");
            });

            modelBuilder.Entity<ArticleContent>(entity =>
            {
                entity.ToTable("articleContent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleId).HasColumnName("articleID");

                entity.Property(e => e.ContentOrder).HasColumnName("contentOrder");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(50)
                    .HasColumnName("contentType");

                entity.Property(e => e.PictureId).HasColumnName("pictureID");

                entity.Property(e => e.TextContent).HasColumnName("textContent");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleContents)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__articleCo__artic__693CA210");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.ArticleContents)
                    .HasForeignKey(d => d.PictureId)
                    .HasConstraintName("FK__articleCo__pictu__6A30C649");
            });

            modelBuilder.Entity<ArticleOutline>(entity =>
            {
                entity.ToTable("articleOutline");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleClass).HasColumnName("articleClass");

                entity.Property(e => e.ArticleSource)
                    .HasMaxLength(50)
                    .HasColumnName("articleSource");

                entity.Property(e => e.Intro)
                    .HasMaxLength(255)
                    .HasColumnName("intro");

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(255)
                    .HasColumnName("pictureURL");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasColumnName("publishDate");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasOne(d => d.ArticleClassNavigation)
                    .WithMany(p => p.ArticleOutlines)
                    .HasForeignKey(d => d.ArticleClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__articleOu__artic__6B24EA82");

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Articles)
                    .UsingEntity<Dictionary<string, object>>(
                        "ArticleTag",
                        l => l.HasOne<ArticleTagList>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__articleTa__tagID__6D0D32F4"),
                        r => r.HasOne<ArticleOutline>().WithMany().HasForeignKey("ArticleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__articleTa__artic__6C190EBB"),
                        j =>
                        {
                            j.HasKey("ArticleId", "TagId").HasName("PK__articleT__10DC1349FB49E001");

                            j.ToTable("articleTag");

                            j.IndexerProperty<int>("ArticleId").HasColumnName("articleID");

                            j.IndexerProperty<int>("TagId").HasColumnName("tagID");
                        });
            });

            modelBuilder.Entity<ArticlePicture>(entity =>
            {
                entity.ToTable("articlePicture");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(255)
                    .HasColumnName("pictureURL");
            });

            modelBuilder.Entity<ArticleTagList>(entity =>
            {
                entity.ToTable("articleTagList");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TagName)
                    .HasMaxLength(20)
                    .HasColumnName("tagName");
            });

            modelBuilder.Entity<BookedCase>(entity =>
            {
                entity.ToTable("bookedCase");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.BookedDate)
                    .HasColumnType("date")
                    .HasColumnName("bookedDate");

                entity.Property(e => e.BuildingId).HasColumnName("buildingID");

                entity.Property(e => e.ClickDate)
                    .HasColumnType("datetime")
                    .HasColumnName("clickDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BookedCases)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookedCas__accou__6E01572D");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.BookedCases)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bookedCas__build__6EF57B66");
            });

            modelBuilder.Entity<Buildingdb>(entity =>
            {
                entity.HasKey(e => e.建案序號);

                entity.ToTable("buildingdb");

                entity.Property(e => e.建案序號).ValueGeneratedNever();

                entity.Property(e => e.PicUrl)
                    .HasMaxLength(255)
                    .HasColumnName("picURL");

                entity.Property(e => e.地區).HasMaxLength(255);

                entity.Property(e => e.坪數規劃).HasMaxLength(255);

                entity.Property(e => e.基地位置).HasMaxLength(255);

                entity.Property(e => e.基地面積).HasMaxLength(255);

                entity.Property(e => e.建案名稱).HasMaxLength(255);

                entity.Property(e => e.房型規劃).HasMaxLength(255);

                entity.Property(e => e.接待中心).HasMaxLength(255);

                entity.Property(e => e.樓層規劃).HasMaxLength(255);

                entity.Property(e => e.營造公司).HasMaxLength(255);

                entity.Property(e => e.特點).HasMaxLength(255);

                entity.Property(e => e.現況).HasMaxLength(255);

                entity.Property(e => e.縣市).HasMaxLength(255);

                entity.Property(e => e.諮詢專線).HasMaxLength(255);
            });

            modelBuilder.Entity<Buildingdetail>(entity =>
            {
                entity.HasKey(e => e.建案序號);

                entity.ToTable("buildingdetail");

                entity.Property(e => e.建案序號).ValueGeneratedNever();

                entity.Property(e => e.Pic1)
                    .HasMaxLength(255)
                    .HasColumnName("pic1");

                entity.Property(e => e.Pic10)
                    .HasMaxLength(255)
                    .HasColumnName("pic10");

                entity.Property(e => e.Pic2)
                    .HasMaxLength(255)
                    .HasColumnName("pic2");

                entity.Property(e => e.Pic3)
                    .HasMaxLength(255)
                    .HasColumnName("pic3");

                entity.Property(e => e.Pic4)
                    .HasMaxLength(255)
                    .HasColumnName("pic4");

                entity.Property(e => e.Pic5)
                    .HasMaxLength(255)
                    .HasColumnName("pic5");

                entity.Property(e => e.Pic6)
                    .HasMaxLength(255)
                    .HasColumnName("pic6");

                entity.Property(e => e.Pic7)
                    .HasMaxLength(255)
                    .HasColumnName("pic7");

                entity.Property(e => e.Pic8)
                    .HasMaxLength(255)
                    .HasColumnName("pic8");

                entity.Property(e => e.Pic9)
                    .HasMaxLength(255)
                    .HasColumnName("pic9");

                entity.Property(e => e.地區).HasMaxLength(255);

                entity.Property(e => e.建案名稱).HasMaxLength(255);

                entity.Property(e => e.文宣1).HasMaxLength(255);

                entity.Property(e => e.文宣2).HasMaxLength(255);

                entity.Property(e => e.文宣3).HasMaxLength(255);

                entity.Property(e => e.文宣4).HasMaxLength(255);

                entity.Property(e => e.文宣5).HasMaxLength(255);

                entity.Property(e => e.文宣6).HasMaxLength(255);

                entity.Property(e => e.文宣7).HasMaxLength(255);

                entity.Property(e => e.文宣8).HasMaxLength(255);

                entity.Property(e => e.文宣9).HasMaxLength(255);

                entity.Property(e => e.縣市).HasMaxLength(255);
            });

            modelBuilder.Entity<CityList>(entity =>
            {
                entity.ToTable("cityList");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityName)
                    .HasMaxLength(10)
                    .HasColumnName("cityName");
            });

            modelBuilder.Entity<ClientConnection>(entity =>
            {
                entity.ToTable("ClientConnection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConnectionId)
                    .HasMaxLength(50)
                    .HasColumnName("connectionId");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ClientConnections)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ClientConnection_ServiceConnection");
            });

            modelBuilder.Entity<CollectArticle>(entity =>
            {
                entity.ToTable("collectArticle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.ArticleId).HasColumnName("articleID");

                entity.Property(e => e.CollectDate)
                    .HasColumnType("datetime")
                    .HasColumnName("collectDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CollectArticles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__collectAr__accou__70DDC3D8");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.CollectArticles)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__collectAr__artic__71D1E811");
            });

            modelBuilder.Entity<CollectBuilding>(entity =>
            {
                entity.ToTable("collectBuilding");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.BuildingId).HasColumnName("buildingID");

                entity.Property(e => e.CollectDate)
                    .HasColumnType("datetime")
                    .HasColumnName("collectDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CollectBuildings)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__collectBu__accou__72C60C4A");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.CollectBuildings)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__collectBu__build__73BA3083");
            });

            modelBuilder.Entity<ForgetCode>(entity =>
            {
                entity.ToTable("forgetCode");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.TheCode)
                    .HasMaxLength(50)
                    .HasColumnName("theCode");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ForgetCodes)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__forgetCod__accou__74AE54BC");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_Id");

                entity.Property(e => e.Content)
                    .HasMaxLength(200)
                    .HasColumnName("content");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Message__client___75A278F5");
            });

            modelBuilder.Entity<RegionList>(entity =>
            {
                entity.ToTable("regionList");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityId).HasColumnName("cityID");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(10)
                    .HasColumnName("regionName");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.RegionLists)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__regionLis__cityI__76969D2E");
            });

            modelBuilder.Entity<ServiceConnection>(entity =>
            {
                entity.ToTable("ServiceConnection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConnectionId)
                    .HasMaxLength(50)
                    .HasColumnName("connectionId");

                entity.Property(e => e.OnlineCount).HasColumnName("online_count");
            });

            modelBuilder.Entity<ViewedArticle>(entity =>
            {
                entity.ToTable("viewedArticle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.ArticleId).HasColumnName("articleID");

                entity.Property(e => e.ViewDate)
                    .HasColumnType("datetime")
                    .HasColumnName("viewDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ViewedArticles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__viewedArt__accou__778AC167");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ViewedArticles)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__viewedArt__artic__787EE5A0");
            });

            modelBuilder.Entity<ViewedBuilding>(entity =>
            {
                entity.ToTable("viewedBuilding");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.BuildingId).HasColumnName("buildingID");

                entity.Property(e => e.ViewDate)
                    .HasColumnType("datetime")
                    .HasColumnName("viewDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ViewedBuildings)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__viewedBui__accou__797309D9");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.ViewedBuildings)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__viewedBui__build__7A672E12");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
