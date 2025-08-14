using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;


namespace LibraryManagementAPI.Data
{
    public class LibraryDbContext : IdentityDbContext<User>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookInquiry> BookInquiries { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<AttemptAnswer> AttemptAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.TitleArabic).HasMaxLength(200);
                entity.Property(e => e.ISBN).HasMaxLength(20);
                entity.Property(e => e.Language).HasMaxLength(20).HasDefaultValue("Arabic");
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Rating).HasColumnType("decimal(3,2)").HasDefaultValue(0.0m);
                entity.Property(e => e.StockQuantity).HasDefaultValue(0);
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
                entity.Property(e => e.IsFeatured).HasDefaultValue(false);
                entity.Property(e => e.IsNewRelease).HasDefaultValue(false);
                entity.Property(e => e.RatingCount).HasDefaultValue(0);
                entity.Property(e => e.ViewCount).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(e => e.ISBN).IsUnique();
                entity.HasIndex(e => e.Title);
                entity.HasIndex(e => e.IsAvailable);
                entity.HasIndex(e => e.IsFeatured);
                entity.HasIndex(e => e.IsNewRelease);
            });

            



            // Configure Author entity
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NameArabic).HasMaxLength(100);
                entity.Property(e => e.Nationality).HasMaxLength(50);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasIndex(e => e.Name);
            });

            // Configure Publisher entity
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NameArabic).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Website).HasMaxLength(200);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasIndex(e => e.Name);
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NameArabic).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Icon).HasMaxLength(50);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasIndex(e => e.Name);
            });

            // Configure BookImage entity
            modelBuilder.Entity<BookImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ImageType).HasDefaultValue(ImageType.Gallery);
                entity.Property(e => e.DisplayOrder).HasDefaultValue(0);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookImages)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.BookId);
            });

            // Configure BookInquiry entity
            modelBuilder.Entity<BookInquiry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerName).HasMaxLength(100);
                entity.Property(e => e.CustomerPhone).HasMaxLength(20);
                entity.Property(e => e.CustomerEmail).HasMaxLength(100);
                entity.Property(e => e.Status).HasDefaultValue(InquiryStatus.Pending);
                entity.Property(e => e.WhatsAppMessageSent).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookInquiries)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookInquiries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(e => e.BookId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CreatedAt);
            });

            // Configure SystemSetting entity
            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SettingKey).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasIndex(e => e.SettingKey).IsUnique();
            });

            // Configure User entity (extends IdentityUser)
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");
            });

            // Configure Quiz entity
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.TitleArabic).HasMaxLength(200);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Grade).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Chapter).HasMaxLength(100);
                entity.Property(e => e.TimeLimit).HasDefaultValue(30);
                entity.Property(e => e.PassingScore).HasDefaultValue(60);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsPublic).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CreatedQuizzes)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(e => e.Subject);
                entity.HasIndex(e => e.Grade);
                entity.HasIndex(e => e.IsActive);
            });

            // Configure QuizQuestion entity
            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.QuestionText).IsRequired();
                entity.Property(e => e.QuestionTextArabic);
                entity.Property(e => e.Points).HasDefaultValue(1);
                entity.Property(e => e.OrderIndex).HasDefaultValue(0);
                entity.Property(e => e.Type).HasDefaultValue(QuestionType.MultipleChoice);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.QuizId);
                entity.HasIndex(e => e.OrderIndex);
            });

            // Configure QuestionOption entity
            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OptionText).IsRequired();
                entity.Property(e => e.OptionTextArabic);
                entity.Property(e => e.IsCorrect).HasDefaultValue(false);
                entity.Property(e => e.OrderIndex).HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.QuestionId);
                entity.HasIndex(e => e.IsCorrect);
            });

            // Configure QuizAttempt entity
            modelBuilder.Entity<QuizAttempt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.StartedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.Score).HasDefaultValue(0);
                entity.Property(e => e.TotalScore).HasDefaultValue(0);
                entity.Property(e => e.Percentage).HasDefaultValue(0);
                entity.Property(e => e.IsPassed).HasDefaultValue(false);
                entity.Property(e => e.TimeSpent).HasDefaultValue(0);
                entity.Property(e => e.Status).HasDefaultValue(AttemptStatus.InProgress);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Attempts)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuizAttempts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.QuizId);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.StartedAt);
            });

            // Configure AttemptAnswer entity
            modelBuilder.Entity<AttemptAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AttemptId).IsRequired();
                entity.Property(e => e.QuestionId).IsRequired();
                entity.Property(e => e.IsCorrect).HasDefaultValue(false);
                entity.Property(e => e.PointsEarned).HasDefaultValue(0);
                entity.Property(e => e.AnsweredAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");

                entity.HasOne(d => d.Attempt)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.AttemptId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Question)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.SelectedOption)
                    .WithMany()
                    .HasForeignKey(d => d.SelectedOptionId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(e => e.AttemptId);
                entity.HasIndex(e => e.QuestionId);
            });

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Ahmed Shawqi", NameArabic = "أحمد شوقي", Nationality = "Egyptian", Biography = "Egyptian poet and playwright", BiographyArabic = "شاعر وكاتب مسرحي مصري", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Author { Id = 2, Name = "Taha Hussein", NameArabic = "طه حسين", Nationality = "Egyptian", Biography = "Egyptian writer and intellectual", BiographyArabic = "كاتب ومفكر مصري", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Author { Id = 3, Name = "Zaki Naguib Mahmoud", NameArabic = "زكي نجيب محمود", Nationality = "Egyptian", Biography = "Egyptian philosopher and writer", BiographyArabic = "فيلسوف وكاتب مصري", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Author { Id = 4, Name = "Naguib Mahfouz", NameArabic = "نجيب محفوظ", Nationality = "Egyptian", Biography = "Nobel Prize-winning Egyptian writer", BiographyArabic = "كاتب مصري حائز على جائزة نوبل", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Author { Id = 5, Name = "Khalil Gibran", NameArabic = "جبران خليل جبران", Nationality = "Lebanese", Biography = "Lebanese-American writer and poet", BiographyArabic = "كاتب وشاعر لبناني أمريكي", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Dar Al-Ma'arif", NameArabic = "دار المعارف", Address = "Cairo, Egypt", Phone = "+20123456789", Email = "info@daralmaarif.com", Website = "www.daralmaarif.com", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Publisher { Id = 2, Name = "Dar Al-Shorouk", NameArabic = "دار الشروق", Address = "Cairo, Egypt", Phone = "+20123456790", Email = "info@shorouk.com", Website = "www.shorouk.com", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Publisher { Id = 3, Name = "Dar Al-Kitab Al-Arabi", NameArabic = "دار الكتاب العربي", Address = "Beirut, Lebanon", Phone = "+96112345678", Email = "info@arabicbook.com", Website = "www.arabicbook.com", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Publisher { Id = 4, Name = "Dar Al-Fikr", NameArabic = "دار الفكر", Address = "Damascus, Syria", Phone = "+96312345678", Email = "info@alfikr.com", Website = "www.alfikr.com", IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Literature", NameArabic = "الأدب والرواية", Icon = "fas fa-feather-alt", Description = "Fiction and literature books", DescriptionArabic = "كتب الأدب والرواية" },
                new Category { Id = 2, Name = "Science", NameArabic = "العلوم", Icon = "fas fa-flask", Description = "Science and research books", DescriptionArabic = "كتب العلوم والبحث" },
                new Category { Id = 3, Name = "History", NameArabic = "التاريخ", Icon = "fas fa-landmark", Description = "History and civilization books", DescriptionArabic = "كتب التاريخ والحضارة" },
                new Category { Id = 4, Name = "Religion", NameArabic = "الدين", Icon = "fas fa-mosque", Description = "Religious and Islamic studies", DescriptionArabic = "الكتب الدينية والدراسات الإسلامية" },
                new Category { Id = 5, Name = "Technology", NameArabic = "التكنولوجيا", Icon = "fas fa-laptop-code", Description = "Technology and programming books", DescriptionArabic = "كتب التكنولوجيا والبرمجة" },
                new Category { Id = 6, Name = "Philosophy", NameArabic = "الفلسفة", Icon = "fas fa-brain", Description = "Philosophy and thought books", DescriptionArabic = "كتب الفلسفة والفكر" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { 
                    Id = 1, 
                    Title = "The Prophet", 
                    TitleArabic = "النبي", 
                    AuthorId = 5, 
                    PublisherId = 3, 
                    CategoryId = 6, 
                    ISBN = "978-0-394-71585-9", 
                    Language = "Arabic", 
                    Description = "A book of 26 prose poetry essays written in English by the Lebanese-American poet and writer Kahlil Gibran", 
                    DescriptionArabic = "كتاب يحتوي على 26 مقالة شعرية نثرية كتبها الشاعر والكاتب اللبناني الأمريكي جبران خليل جبران", 
                    Price = 45.00m, 
                    OriginalPrice = 50.00m, 
                    StockQuantity = 25, 
                    IsAvailable = true, 
                    IsFeatured = true, 
                    IsNewRelease = false, 
                    Rating = 4.8m, 
                    RatingCount = 150, 
                    ViewCount = 500, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                },
                new Book { 
                    Id = 2, 
                    Title = "The Days", 
                    TitleArabic = "الأيام", 
                    AuthorId = 2, 
                    PublisherId = 1, 
                    CategoryId = 1, 
                    ISBN = "978-977-02-1234-5", 
                    Language = "Arabic", 
                    Description = "An autobiographical novel by Taha Hussein", 
                    DescriptionArabic = "رواية سيرة ذاتية لطه حسين", 
                    Price = 35.00m, 
                    OriginalPrice = 40.00m, 
                    StockQuantity = 15, 
                    IsAvailable = true, 
                    IsFeatured = false, 
                    IsNewRelease = true, 
                    Rating = 4.5m, 
                    RatingCount = 89, 
                    ViewCount = 320, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                },
                new Book { 
                    Id = 3, 
                    Title = "Philosophy of Science", 
                    TitleArabic = "فلسفة العلوم", 
                    AuthorId = 3, 
                    PublisherId = 2, 
                    CategoryId = 6, 
                    ISBN = "978-977-09-5678-9", 
                    Language = "Arabic", 
                    Description = "An introduction to the philosophy of science", 
                    DescriptionArabic = "مقدمة في فلسفة العلوم", 
                    Price = 55.00m, 
                    OriginalPrice = 60.00m, 
                    StockQuantity = 8, 
                    IsAvailable = true, 
                    IsFeatured = false, 
                    IsNewRelease = false, 
                    Rating = 4.2m, 
                    RatingCount = 45, 
                    ViewCount = 180, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                },
                new Book { 
                    Id = 4, 
                    Title = "Children of Gebelawi", 
                    TitleArabic = "أولاد حارتنا", 
                    AuthorId = 4, 
                    PublisherId = 1, 
                    CategoryId = 1, 
                    ISBN = "978-977-02-9012-3", 
                    Language = "Arabic", 
                    Description = "A novel by Nobel Prize winner Naguib Mahfouz", 
                    DescriptionArabic = "رواية للكاتب نجيب محفوظ الحائز على جائزة نوبل", 
                    Price = 40.00m, 
                    OriginalPrice = 45.00m, 
                    StockQuantity = 12, 
                    IsAvailable = true, 
                    IsFeatured = true, 
                    IsNewRelease = false, 
                    Rating = 4.7m, 
                    RatingCount = 120, 
                    ViewCount = 450, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                },
                new Book { 
                    Id = 5, 
                    Title = "Poetry Collection", 
                    TitleArabic = "ديوان شعر", 
                    AuthorId = 1, 
                    PublisherId = 4, 
                    CategoryId = 1, 
                    ISBN = "978-963-12-3456-7", 
                    Language = "Arabic", 
                    Description = "Collection of poems by Ahmed Shawqi", 
                    DescriptionArabic = "مجموعة شعرية لأحمد شوقي", 
                    Price = 30.00m, 
                    OriginalPrice = 35.00m, 
                    StockQuantity = 20, 
                    IsAvailable = true, 
                    IsFeatured = false, 
                    IsNewRelease = false, 
                    Rating = 4.3m, 
                    RatingCount = 67, 
                    ViewCount = 250, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                }
            );

            // Seed System Settings
            modelBuilder.Entity<SystemSetting>().HasData(
                new SystemSetting { Id = 1, SettingKey = "WhatsAppPhoneNumber", SettingValue = "+962785462983", Description = "Library WhatsApp contact number" },
                new SystemSetting { Id = 2, SettingKey = "WhatsAppMessageTemplate", SettingValue = "مرحباً، أود الاستفسار عن توفر كتاب: {BookTitle} - {BookAuthor}", Description = "WhatsApp message template for book inquiries" },
                new SystemSetting { Id = 3, SettingKey = "LibraryName", SettingValue = "ROYAL STUDY", Description = "Library name" },
                new SystemSetting { Id = 4, SettingKey = "LibraryNameEnglish", SettingValue = "ROYAL STUDY", Description = "Library name in English" },
                new SystemSetting { Id = 5, SettingKey = "ContactEmail", SettingValue = "info@royalstudy.com", Description = "Library contact email" },
                new SystemSetting { Id = 6, SettingKey = "ContactPhone", SettingValue = "+962785462983", Description = "Library contact phone" },
                new SystemSetting { Id = 7, SettingKey = "LibraryAddress", SettingValue = "إربد، الأردن", Description = "Library address" },
                new SystemSetting { Id = 8, SettingKey = "LibraryAddressEnglish", SettingValue = "Irbid, Jordan", Description = "Library address in English" },
                new SystemSetting { Id = 9, SettingKey = "Currency", SettingValue = "د.أ", Description = "Library currency" },
                new SystemSetting { Id = 10, SettingKey = "CurrencyEnglish", SettingValue = "JOD", Description = "Library currency in English" }
            );

            // Seed Sample Quizzes
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz { 
                    Id = 1, 
                    Title = "Mathematics Quiz - Grade 10", 
                    TitleArabic = "كويز الرياضيات - الصف العاشر", 
                    Description = "Basic mathematics quiz for 10th grade students", 
                    DescriptionArabic = "كويز أساسيات الرياضيات لطلاب الصف العاشر", 
                    Subject = "Mathematics", 
                    Grade = "Grade 10", 
                    Chapter = "Algebra", 
                    TimeLimit = 20, 
                    TotalQuestions = 5, 
                    PassingScore = 60, 
                    IsActive = true, 
                    IsPublic = true, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                },
                new Quiz { 
                    Id = 2, 
                    Title = "Physics Quiz - Tawjihi", 
                    TitleArabic = "كويز الفيزياء - التوجيهي", 
                    Description = "Physics fundamentals for Tawjihi students", 
                    DescriptionArabic = "أساسيات الفيزياء لطلاب التوجيهي", 
                    Subject = "Physics", 
                    Grade = "Tawjihi", 
                    Chapter = "Mechanics", 
                    TimeLimit = 25, 
                    TotalQuestions = 6, 
                    PassingScore = 70, 
                    IsActive = true, 
                    IsPublic = true, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                },
                new Quiz { 
                    Id = 3, 
                    Title = "Chemistry Quiz - Grade 11", 
                    TitleArabic = "كويز الكيمياء - الصف الحادي عشر", 
                    Description = "Chemistry basics for 11th grade students", 
                    DescriptionArabic = "أساسيات الكيمياء لطلاب الصف الحادي عشر", 
                    Subject = "Chemistry", 
                    Grade = "Grade 11", 
                    Chapter = "Atomic Structure", 
                    TimeLimit = 15, 
                    TotalQuestions = 4, 
                    PassingScore = 75, 
                    IsActive = true, 
                    IsPublic = true, 
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                }
            );

            // Seed Sample Questions for Quiz 1 (Mathematics)
            modelBuilder.Entity<QuizQuestion>().HasData(
                new QuizQuestion { Id = 1, QuizId = 1, QuestionText = "What is the value of x in the equation 2x + 5 = 13?", QuestionTextArabic = "ما قيمة س في المعادلة 2س + 5 = 13؟", Points = 1, OrderIndex = 1, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 2, QuizId = 1, QuestionText = "Solve: 3x - 7 = 8", QuestionTextArabic = "حل المعادلة: 3س - 7 = 8", Points = 1, OrderIndex = 2, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 3, QuizId = 1, QuestionText = "The square root of 16 is 4", QuestionTextArabic = "الجذر التربيعي لـ 16 هو 4", Points = 1, OrderIndex = 3, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 4, QuizId = 1, QuestionText = "What is 15% of 200?", QuestionTextArabic = "ما هو 15% من 200؟", Points = 1, OrderIndex = 4, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 5, QuizId = 1, QuestionText = "The sum of angles in a triangle is 180 degrees", QuestionTextArabic = "مجموع زوايا المثلث 180 درجة", Points = 1, OrderIndex = 5, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Sample Questions for Quiz 2 (Physics)
            modelBuilder.Entity<QuizQuestion>().HasData(
                new QuizQuestion { Id = 6, QuizId = 2, QuestionText = "What is the SI unit of force?", QuestionTextArabic = "ما هي وحدة القوة في النظام الدولي؟", Points = 1, OrderIndex = 1, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 7, QuizId = 2, QuestionText = "Newton's first law is also known as the law of inertia", QuestionTextArabic = "قانون نيوتن الأول يعرف أيضاً بقانون القصور الذاتي", Points = 1, OrderIndex = 2, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 8, QuizId = 2, QuestionText = "What is the formula for kinetic energy?", QuestionTextArabic = "ما هي معادلة الطاقة الحركية؟", Points = 1, OrderIndex = 3, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 9, QuizId = 2, QuestionText = "Gravity is a force that pulls objects toward the center of the Earth", QuestionTextArabic = "الجاذبية قوة تجذب الأجسام نحو مركز الأرض", Points = 1, OrderIndex = 4, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 10, QuizId = 2, QuestionText = "What is the unit of power?", QuestionTextArabic = "ما هي وحدة القدرة؟", Points = 1, OrderIndex = 5, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 11, QuizId = 2, QuestionText = "Velocity is a scalar quantity", QuestionTextArabic = "السرعة كمية قياسية", Points = 1, OrderIndex = 6, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Sample Questions for Quiz 3 (Chemistry)
            modelBuilder.Entity<QuizQuestion>().HasData(
                new QuizQuestion { Id = 12, QuizId = 3, QuestionText = "What is the atomic number of hydrogen?", QuestionTextArabic = "ما هو العدد الذري للهيدروجين؟", Points = 1, OrderIndex = 1, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 13, QuizId = 3, QuestionText = "The nucleus contains protons and neutrons", QuestionTextArabic = "النواة تحتوي على البروتونات والنيوترونات", Points = 1, OrderIndex = 2, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 14, QuizId = 3, QuestionText = "What is the chemical symbol for gold?", QuestionTextArabic = "ما هو الرمز الكيميائي للذهب؟", Points = 1, OrderIndex = 3, Type = QuestionType.MultipleChoice, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuizQuestion { Id = 15, QuizId = 3, QuestionText = "Electrons have a positive charge", QuestionTextArabic = "الإلكترونات لها شحنة موجبة", Points = 1, OrderIndex = 4, Type = QuestionType.TrueFalse, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Options for Mathematics Questions
            modelBuilder.Entity<QuestionOption>().HasData(
                // Question 1 options
                new QuestionOption { Id = 1, QuestionId = 1, OptionText = "4", OptionTextArabic = "4", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 2, QuestionId = 1, OptionText = "6", OptionTextArabic = "6", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 3, QuestionId = 1, OptionText = "8", OptionTextArabic = "8", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 4, QuestionId = 1, OptionText = "10", OptionTextArabic = "10", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                
                // Question 2 options
                new QuestionOption { Id = 5, QuestionId = 2, OptionText = "5", OptionTextArabic = "5", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 6, QuestionId = 2, OptionText = "3", OptionTextArabic = "3", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 7, QuestionId = 2, OptionText = "7", OptionTextArabic = "7", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 8, QuestionId = 2, OptionText = "9", OptionTextArabic = "9", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                
                // Question 4 options
                new QuestionOption { Id = 9, QuestionId = 4, OptionText = "30", OptionTextArabic = "30", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 10, QuestionId = 4, OptionText = "25", OptionTextArabic = "25", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 11, QuestionId = 4, OptionText = "35", OptionTextArabic = "35", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 12, QuestionId = 4, OptionText = "40", OptionTextArabic = "40", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Options for Physics Questions
            modelBuilder.Entity<QuestionOption>().HasData(
                // Question 6 options
                new QuestionOption { Id = 13, QuestionId = 6, OptionText = "Newton", OptionTextArabic = "نيوتن", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 14, QuestionId = 6, OptionText = "Joule", OptionTextArabic = "جول", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 15, QuestionId = 6, OptionText = "Watt", OptionTextArabic = "واط", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 16, QuestionId = 6, OptionText = "Pascal", OptionTextArabic = "باسكال", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                
                // Question 8 options
                new QuestionOption { Id = 17, QuestionId = 8, OptionText = "KE = 1/2 mv²", OptionTextArabic = "ط ح = 1/2 ك ع²", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 18, QuestionId = 8, OptionText = "KE = mv", OptionTextArabic = "ط ح = ك ع", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 19, QuestionId = 8, OptionText = "KE = mgh", OptionTextArabic = "ط ح = ك ج ع", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 20, QuestionId = 8, OptionText = "KE = Fd", OptionTextArabic = "ط ح = ق ف", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                
                // Question 10 options
                new QuestionOption { Id = 21, QuestionId = 10, OptionText = "Watt", OptionTextArabic = "واط", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 22, QuestionId = 10, OptionText = "Joule", OptionTextArabic = "جول", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 23, QuestionId = 10, OptionText = "Newton", OptionTextArabic = "نيوتن", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 24, QuestionId = 10, OptionText = "Meter", OptionTextArabic = "متر", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // Seed Options for Chemistry Questions
            modelBuilder.Entity<QuestionOption>().HasData(
                // Question 12 options
                new QuestionOption { Id = 25, QuestionId = 12, OptionText = "1", OptionTextArabic = "1", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 26, QuestionId = 12, OptionText = "2", OptionTextArabic = "2", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 27, QuestionId = 12, OptionText = "3", OptionTextArabic = "3", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 28, QuestionId = 12, OptionText = "4", OptionTextArabic = "4", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                
                // Question 14 options
                new QuestionOption { Id = 29, QuestionId = 14, OptionText = "Au", OptionTextArabic = "Au", IsCorrect = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 30, QuestionId = 14, OptionText = "Ag", OptionTextArabic = "Ag", IsCorrect = false, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 31, QuestionId = 14, OptionText = "Cu", OptionTextArabic = "Cu", IsCorrect = false, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new QuestionOption { Id = 32, QuestionId = 14, OptionText = "Fe", OptionTextArabic = "Fe", IsCorrect = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }
    }
}

