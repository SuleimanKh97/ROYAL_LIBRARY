using MySqlConnector;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=LibraryManagementDB;User=libraryuser;Password=LibraryPass123;Port=3306;";
        
        try
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            Console.WriteLine("Connected to database successfully!");
            
            // Insert sample study tips
            string[] insertTipsStatements = {
                @"INSERT INTO `StudyTips` (`Category`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subject`, `Tips`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('grade-selection', 'How to Choose Your Grade', 'كيفية اختيار الصف الدراسي', 'Comprehensive guide for choosing the right grade and subjects', 'دليل شامل لاختيار الصف والمواد الدراسية المناسبة', 'first-year', NULL, '[""تقييم قدراتك الأكاديمية"", ""استشارة المرشد الأكاديمي"", ""دراسة متطلبات الجامعة"", ""التوازن بين المواد العلمية والأدبية""]', 1, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudyTips` (`Category`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subject`, `Tips`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('daily-schedule', 'Daily Study Schedule', 'الجدول اليومي للدراسة', 'Optimal daily schedule for effective studying', 'الجدول اليومي الأمثل للدراسة الفعالة', 'first-year', NULL, '[""الاستيقاظ مبكراً"", ""تقسيم الوقت بين المواد"", ""أخذ فترات راحة منتظمة"", ""المراجعة المسائية""]', 2, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudyTips` (`Category`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subject`, `Tips`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('weekly-schedule', 'Weekly Study Plan', 'الخطة الأسبوعية للدراسة', 'Weekly planning strategy for better organization', 'استراتيجية التخطيط الأسبوعي لتنظيم أفضل', 'first-year', NULL, '[""تحديد أهداف أسبوعية"", ""توزيع المواد على أيام الأسبوع"", ""تخصيص وقت للمراجعة"", ""التقييم الأسبوعي""]', 3, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudyTips` (`Category`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subject`, `Tips`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('general-tips', 'General Study Tips', 'نصائح عامة للدراسة', 'Essential tips for successful studying', 'نصائح أساسية للدراسة الناجحة', 'first-year', NULL, '[""إنشاء بيئة دراسة هادئة"", ""استخدام تقنيات الحفظ الفعالة"", ""حل تمارين متنوعة"", ""المراجعة المستمرة""]', 4, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudyTips` (`Category`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subject`, `Tips`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('arabic-tips', 'Arabic Language Study Tips', 'نصائح لدراسة اللغة العربية', 'Specialized tips for Arabic language study', 'نصائح متخصصة لدراسة اللغة العربية', 'first-year', 'arabic', '[""قراءة النصوص الأدبية"", ""حفظ القواعد النحوية"", ""ممارسة الكتابة"", ""دراسة البلاغة""]', 5, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudyTips` (`Category`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subject`, `Tips`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('english-tips', 'English Language Study Tips', 'نصائح لدراسة اللغة الإنجليزية', 'Effective strategies for English language learning', 'استراتيجيات فعالة لتعلم اللغة الإنجليزية', 'first-year', 'english', '[""ممارسة المحادثة"", ""قراءة الكتب الإنجليزية"", ""حفظ المفردات"", ""حل تمارين القواعد""]', 6, 1, NOW(), NOW())"
            };
            
            // Insert sample study schedules
            string[] insertSchedulesStatements = {
                @"INSERT INTO `StudySchedules` (`Type`, `Day`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subjects`, `Focus`, `FocusArabic`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('daily', 'الأحد', 'Sunday Study Schedule', 'الجدول الدراسي ليوم الأحد', 'Complete daily schedule for Sunday', 'الجدول اليومي الكامل ليوم الأحد', 'first-year', '[""arabic"", ""english"", ""islamic""]', 'Language Focus', 'التركيز على اللغات', 1, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudySchedules` (`Type`, `Day`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subjects`, `Focus`, `FocusArabic`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('daily', 'الاثنين', 'Monday Study Schedule', 'الجدول الدراسي ليوم الاثنين', 'Complete daily schedule for Monday', 'الجدول اليومي الكامل ليوم الاثنين', 'first-year', '[""history"", ""math"", ""science""]', 'Science Focus', 'التركيز على العلوم', 2, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudySchedules` (`Type`, `Day`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subjects`, `Focus`, `FocusArabic`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('daily', 'الثلاثاء', 'Tuesday Study Schedule', 'الجدول الدراسي ليوم الثلاثاء', 'Complete daily schedule for Tuesday', 'الجدول اليومي الكامل ليوم الثلاثاء', 'first-year', '[""arabic"", ""english"", ""history""]', 'Mixed Focus', 'التركيز المختلط', 3, 1, NOW(), NOW())",
                
                @"INSERT INTO `StudySchedules` (`Type`, `Day`, `Title`, `TitleArabic`, `Description`, `DescriptionArabic`, `Grade`, `Subjects`, `Focus`, `FocusArabic`, `OrderIndex`, `IsActive`, `CreatedAt`, `UpdatedAt`) VALUES
                ('weekly', 'الأسبوع', 'Weekly Study Plan', 'الخطة الدراسية الأسبوعية', 'Complete weekly study plan', 'الخطة الدراسية الأسبوعية الكاملة', 'first-year', '[""arabic"", ""english"", ""islamic"", ""history"", ""math"", ""science""]', 'Comprehensive Review', 'المراجعة الشاملة', 4, 1, NOW(), NOW())"
            };
            
            // Execute study tips insertions
            foreach (string statement in insertTipsStatements)
            {
                using var command = new MySqlCommand(statement, connection);
                await command.ExecuteNonQueryAsync();
                Console.WriteLine($"Inserted study tip: {statement.Substring(statement.IndexOf("'") + 1, statement.IndexOf("'", statement.IndexOf("'") + 1) - statement.IndexOf("'") - 1)}");
            }
            
            // Execute study schedules insertions
            foreach (string statement in insertSchedulesStatements)
            {
                using var command = new MySqlCommand(statement, connection);
                await command.ExecuteNonQueryAsync();
                Console.WriteLine($"Inserted study schedule: {statement.Substring(statement.IndexOf("'") + 1, statement.IndexOf("'", statement.IndexOf("'") + 1) - statement.IndexOf("'") - 1)}");
            }
            
            Console.WriteLine("Sample study data inserted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
