using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolSchedule.Infrastructure.Data;

namespace SchoolSchedule.Infrastructure.Migrations
{
    [DbContext(typeof(SchoolScheduleContext))]
    partial class SchoolScheduleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Attendance", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime2");

                b.Property<int>("LessonId")
                    .HasColumnType("int");

                b.Property<int>("StudentId")
                    .HasColumnType("int");

                b.Property<string>("Type")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("LessonId");

                b.HasIndex("StudentId");

                b.ToTable("Attendances");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Grade", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<int>("GradeCategoryId")
                    .HasColumnType("int");

                b.Property<int>("LessonId")
                    .HasColumnType("int");

                b.Property<int>("StudentId")
                    .HasColumnType("int");

                b.Property<int>("Value")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("GradeCategoryId");

                b.HasIndex("LessonId");

                b.HasIndex("StudentId");

                b.ToTable("Grades");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.GradeCategory", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("GradeCategories");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Lesson", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Title")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Lessons");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Student", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Students");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Attendance", b =>
            {
                b.HasOne("SchoolSchedule.Core.Entities.Lesson", "Lesson")
                    .WithMany("Attendances")
                    .HasForeignKey("LessonId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("SchoolSchedule.Core.Entities.Student", "Student")
                    .WithMany("Attendances")
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Lesson");

                b.Navigation("Student");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Grade", b =>
            {
                b.HasOne("SchoolSchedule.Core.Entities.GradeCategory", "GradeCategory")
                    .WithMany("Grades")
                    .HasForeignKey("GradeCategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("SchoolSchedule.Core.Entities.Lesson", "Lesson")
                    .WithMany("Grades")
                    .HasForeignKey("LessonId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("SchoolSchedule.Core.Entities.Student", "Student")
                    .WithMany("Grades")
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("GradeCategory");

                b.Navigation("Lesson");

                b.Navigation("Student");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Lesson", b =>
            {
                b.Navigation("Attendances");

                b.Navigation("Grades");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.GradeCategory", b =>
            {
                b.Navigation("Grades");
            });

            modelBuilder.Entity("SchoolSchedule.Core.Entities.Student", b =>
            {
                b.Navigation("Attendances");

                b.Navigation("Grades");
            });
        }
    }
}