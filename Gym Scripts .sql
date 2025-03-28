/****** Object:  Database [GymDB]    Script Date: 17/8/2024 20:48:09 ******/
CREATE DATABASE [GymDB]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 250 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [GymDB] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [GymDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GymDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GymDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GymDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GymDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [GymDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GymDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GymDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GymDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GymDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GymDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GymDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GymDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GymDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GymDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GymDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GymDB] SET  MULTI_USER 
GO
ALTER DATABASE [GymDB] SET ENCRYPTION ON
GO
ALTER DATABASE [GymDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [GymDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/****** Object:  Table [dbo].[detail_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detail_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoice_id] [int] NOT NULL,
	[user_membership_id] [int] NULL,
	[personal_training_id] [int] NULL,
	[price] [decimal](18, 0) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[discount_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[discount_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[coupon] [nvarchar](50) NOT NULL,
	[percentage] [int] NOT NULL,
	[valid_from] [datetime] NOT NULL,
	[valid_to] [datetime] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equipment_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipment_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[location] [nvarchar](150) NOT NULL,
	[description] [nvarchar](200) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_equipment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[exercise_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exercise_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[equipment_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[sets] [int] NULL,
	[weight] [int] NULL,
	[reps] [int] NULL,
	[duration] [int] NULL,
 CONSTRAINT [PK_exercisee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[group_class_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[group_class_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[class_name] [nvarchar](50) NOT NULL,
	[max_capacity] [int] NOT NULL,
	[current_registered] [int] NOT NULL,
	[class_time] [datetime] NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NOT NULL,
 CONSTRAINT [PK_group_class] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[discount_id] [int] NOT NULL,
	[amount] [decimal](18, 0) NOT NULL,
	[amount_after_discount] [decimal](18, 0) NOT NULL,
	[payment_method] [nvarchar](50) NOT NULL,
	[is_confirmed] [nvarchar](50) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_invoice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[measures_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[measures_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [int] NOT NULL,
	[weight] [decimal](18, 0) NOT NULL,
	[height] [decimal](18, 0) NOT NULL,
	[average_of_fat] [decimal](18, 0) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_measures] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[membership_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[membership_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[amount_classes_allowed] [int] NOT NULL,
	[monthly_cost] [decimal](18, 0) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_membership] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mettings_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mettings_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[time_of_entry] [time](7) NOT NULL,
	[time_of_exit] [time](7) NOT NULL,
	[programmed_date] [datetime] NOT NULL,
	[is_cancelled] [nvarchar](50) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_mettings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notification_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notification_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[notification_content] [nvarchar](250) NOT NULL,
	[was_read] [nvarchar](50) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_notification] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[otp_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[otp_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[otp] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[expire_date] [datetime] NOT NULL,
	[was_used] [nvarchar](50) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_otp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[password_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[password_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[password] [varchar](500) NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_password] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personal_training_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personal_training_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[is_cancelled] [nvarchar](50) NOT NULL,
	[is_paid] [nvarchar](50) NOT NULL,
	[time_of_entry] [time](7) NOT NULL,
	[time_of_exit] [time](7) NOT NULL,
	[programed_date] [datetime] NOT NULL,
	[hourly_rate] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_personal_trainingg] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rol_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[routine_exercise_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routine_exercise_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[exercise_id] [int] NOT NULL,
	[routine_id] [int] NOT NULL,
 CONSTRAINT [PK_routine_exercise] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[routine_progression_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routine_progression_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[routine_id] [int] NOT NULL,
	[exercise_id] [int] NOT NULL,
	[sets] [int] NULL,
	[weight] [int] NULL,
	[reps] [int] NULL,
	[duration] [int] NULL,
	[comments] [nvarchar](200) NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_routine_progression] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[routine_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routine_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_routine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[schedule_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schedule_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[days_of_week] [nvarchar](50) NOT NULL,
	[time_of_entry] [time](7) NOT NULL,
	[time_of_exit] [time](7) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_schedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_group_class_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_group_class_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[group_class_id] [int] NOT NULL,
	[client_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_user_group_class] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_membership_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_membership_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[membership_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_user_membership] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_rol_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_rol_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[rol_id] [int] NOT NULL,
 CONSTRAINT [PK_user_rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_tbl]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[last_login] [datetime] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](50) NOT NULL,
	[birthdate] [datetime] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_user] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[detail_tbl]  WITH CHECK ADD  CONSTRAINT [FK_detail_invoice] FOREIGN KEY([invoice_id])
REFERENCES [dbo].[invoice_tbl] ([id])
GO
ALTER TABLE [dbo].[detail_tbl] CHECK CONSTRAINT [FK_detail_invoice]
GO
ALTER TABLE [dbo].[detail_tbl]  WITH CHECK ADD  CONSTRAINT [FK_detail_personal_training] FOREIGN KEY([personal_training_id])
REFERENCES [dbo].[personal_training_tbl] ([id])
GO
ALTER TABLE [dbo].[detail_tbl] CHECK CONSTRAINT [FK_detail_personal_training]
GO
ALTER TABLE [dbo].[detail_tbl]  WITH CHECK ADD  CONSTRAINT [FK_detail_user_membership] FOREIGN KEY([user_membership_id])
REFERENCES [dbo].[user_membership_tbl] ([id])
GO
ALTER TABLE [dbo].[detail_tbl] CHECK CONSTRAINT [FK_detail_user_membership]
GO
ALTER TABLE [dbo].[exercise_tbl]  WITH CHECK ADD  CONSTRAINT [FK_exercise_equipment] FOREIGN KEY([equipment_id])
REFERENCES [dbo].[equipment_tbl] ([id])
GO
ALTER TABLE [dbo].[exercise_tbl] CHECK CONSTRAINT [FK_exercise_equipment]
GO
ALTER TABLE [dbo].[group_class_tbl]  WITH CHECK ADD  CONSTRAINT [FK_group_class_user] FOREIGN KEY([employee_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[group_class_tbl] CHECK CONSTRAINT [FK_group_class_user]
GO
ALTER TABLE [dbo].[invoice_tbl]  WITH CHECK ADD  CONSTRAINT [FK_invoice_discount] FOREIGN KEY([discount_id])
REFERENCES [dbo].[discount_tbl] ([id])
GO
ALTER TABLE [dbo].[invoice_tbl] CHECK CONSTRAINT [FK_invoice_discount]
GO
ALTER TABLE [dbo].[invoice_tbl]  WITH CHECK ADD  CONSTRAINT [FK_invoice_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[invoice_tbl] CHECK CONSTRAINT [FK_invoice_user]
GO
ALTER TABLE [dbo].[measures_tbl]  WITH CHECK ADD  CONSTRAINT [FK_measures_user] FOREIGN KEY([client_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[measures_tbl] CHECK CONSTRAINT [FK_measures_user]
GO
ALTER TABLE [dbo].[mettings_tbl]  WITH CHECK ADD  CONSTRAINT [FK_mettings_user] FOREIGN KEY([client_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[mettings_tbl] CHECK CONSTRAINT [FK_mettings_user]
GO
ALTER TABLE [dbo].[mettings_tbl]  WITH CHECK ADD  CONSTRAINT [FK_mettings_user1] FOREIGN KEY([employee_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[mettings_tbl] CHECK CONSTRAINT [FK_mettings_user1]
GO
ALTER TABLE [dbo].[notification_tbl]  WITH CHECK ADD  CONSTRAINT [FK_notification_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[notification_tbl] CHECK CONSTRAINT [FK_notification_user]
GO
ALTER TABLE [dbo].[otp_tbl]  WITH CHECK ADD  CONSTRAINT [FK_otp_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[otp_tbl] CHECK CONSTRAINT [FK_otp_user]
GO
ALTER TABLE [dbo].[password_tbl]  WITH CHECK ADD  CONSTRAINT [FK_password_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[password_tbl] CHECK CONSTRAINT [FK_password_user]
GO
ALTER TABLE [dbo].[personal_training_tbl]  WITH CHECK ADD  CONSTRAINT [FK_personal_training_user] FOREIGN KEY([employee_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[personal_training_tbl] CHECK CONSTRAINT [FK_personal_training_user]
GO
ALTER TABLE [dbo].[personal_training_tbl]  WITH CHECK ADD  CONSTRAINT [FK_personal_training_user1] FOREIGN KEY([client_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[personal_training_tbl] CHECK CONSTRAINT [FK_personal_training_user1]
GO
ALTER TABLE [dbo].[routine_exercise_tbl]  WITH CHECK ADD  CONSTRAINT [FK_routine_exercise_exercise] FOREIGN KEY([exercise_id])
REFERENCES [dbo].[exercise_tbl] ([id])
GO
ALTER TABLE [dbo].[routine_exercise_tbl] CHECK CONSTRAINT [FK_routine_exercise_exercise]
GO
ALTER TABLE [dbo].[routine_exercise_tbl]  WITH CHECK ADD  CONSTRAINT [FK_routine_exercise_routine] FOREIGN KEY([routine_id])
REFERENCES [dbo].[routine_tbl] ([id])
GO
ALTER TABLE [dbo].[routine_exercise_tbl] CHECK CONSTRAINT [FK_routine_exercise_routine]
GO
ALTER TABLE [dbo].[routine_progression_tbl]  WITH CHECK ADD  CONSTRAINT [FK_routine_progression_exercise] FOREIGN KEY([exercise_id])
REFERENCES [dbo].[exercise_tbl] ([id])
GO
ALTER TABLE [dbo].[routine_progression_tbl] CHECK CONSTRAINT [FK_routine_progression_exercise]
GO
ALTER TABLE [dbo].[routine_progression_tbl]  WITH CHECK ADD  CONSTRAINT [FK_routine_progression_routine] FOREIGN KEY([routine_id])
REFERENCES [dbo].[routine_tbl] ([id])
GO
ALTER TABLE [dbo].[routine_progression_tbl] CHECK CONSTRAINT [FK_routine_progression_routine]
GO
ALTER TABLE [dbo].[routine_tbl]  WITH CHECK ADD  CONSTRAINT [FK_routine_user] FOREIGN KEY([client_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[routine_tbl] CHECK CONSTRAINT [FK_routine_user]
GO
ALTER TABLE [dbo].[schedule_tbl]  WITH CHECK ADD  CONSTRAINT [FK_schedule_user] FOREIGN KEY([employee_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[schedule_tbl] CHECK CONSTRAINT [FK_schedule_user]
GO
ALTER TABLE [dbo].[user_group_class_tbl]  WITH CHECK ADD  CONSTRAINT [FK_user_group_class_group_class] FOREIGN KEY([group_class_id])
REFERENCES [dbo].[group_class_tbl] ([id])
GO
ALTER TABLE [dbo].[user_group_class_tbl] CHECK CONSTRAINT [FK_user_group_class_group_class]
GO
ALTER TABLE [dbo].[user_group_class_tbl]  WITH CHECK ADD  CONSTRAINT [FK_user_group_class_user] FOREIGN KEY([client_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[user_group_class_tbl] CHECK CONSTRAINT [FK_user_group_class_user]
GO
ALTER TABLE [dbo].[user_membership_tbl]  WITH CHECK ADD  CONSTRAINT [FK_user_membership_membership] FOREIGN KEY([membership_id])
REFERENCES [dbo].[membership_tbl] ([id])
GO
ALTER TABLE [dbo].[user_membership_tbl] CHECK CONSTRAINT [FK_user_membership_membership]
GO
ALTER TABLE [dbo].[user_membership_tbl]  WITH CHECK ADD  CONSTRAINT [FK_user_membership_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[user_membership_tbl] CHECK CONSTRAINT [FK_user_membership_user]
GO
ALTER TABLE [dbo].[user_rol_tbl]  WITH CHECK ADD  CONSTRAINT [FK_user_rol_rol] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_tbl] ([id])
GO
ALTER TABLE [dbo].[user_rol_tbl] CHECK CONSTRAINT [FK_user_rol_rol]
GO
ALTER TABLE [dbo].[user_rol_tbl]  WITH CHECK ADD  CONSTRAINT [FK_user_rol_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_tbl] ([id])
GO
ALTER TABLE [dbo].[user_rol_tbl] CHECK CONSTRAINT [FK_user_rol_user]
GO
/****** Object:  StoredProcedure [dbo].[CRE_DETAIL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_DETAIL_PR]
    @P_InvoiceId INT,
    @P_UserMembershipId INT = NULL,
    @P_PersonalTrainingId INT = NULL,
    @P_Price DECIMAL(18, 0)
AS
BEGIN
    INSERT INTO dbo.detail_tbl (invoice_id, user_membership_id, personal_training_id, price, created)
    VALUES (@P_InvoiceId, 
            @P_UserMembershipId, 
            @P_PersonalTrainingId, 
            @P_Price, 
            GETDATE());
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_DISCOUNT_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Create a Discount
CREATE PROCEDURE [dbo].[CRE_DISCOUNT_PR]
    @P_Type NVARCHAR(50),
    @P_Coupon NVARCHAR(50),
    @P_Percentage INT,
    @P_ValidFrom DATETIME,
    @P_ValidTo DATETIME
AS
BEGIN
    INSERT INTO discount_tbl (type, coupon, percentage, valid_from, valid_to, created)
    VALUES (@P_Type, @P_Coupon, @P_Percentage, @P_ValidFrom, @P_ValidTo, GETDATE());

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_EQUIPMENT_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_EQUIPMENT_PR]
	@P_Name NVARCHAR(50),
	@P_Location NVARCHAR(150),
	@P_Description NVARCHAR(50)

AS
	INSERT INTO equipment_tbl(name,location,description,created)
	VALUES(@P_Name,@P_Location,@P_Description,GETDATE());
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[CRE_EXE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRE_EXE_PR]
	@P_Name NVARCHAR(50),
    @P_Type NVARCHAR(50),
	@P_EquipmentId INT,
    @P_Sets INT,
	@P_Reps INT,
    @P_Weight INT,
    @P_Duration INT
AS
BEGIN
    INSERT INTO exercise_tbl (equipment_id, type, name, sets, weight, reps, duration)
    VALUES (@P_EquipmentId, @P_Type, @P_Name, @P_Sets, @P_Weight, @P_Reps, @P_Duration);
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_GROUP_CLASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_GROUP_CLASS_PR]
    @P_EmployeeId INT,
    @P_ClassName NVARCHAR(50),
    @P_MaxCapacity INT,
    @P_CurrentRegistered INT,
    @P_ClassDate DATETIME,
    @P_StartTime TIME(7),
    @P_EndTime TIME(7)
AS
    INSERT INTO group_class_tbl(employee_id, class_name, max_capacity, current_registered, class_time, start_time, end_time)
    VALUES(@P_EmployeeId, @P_ClassName, @P_MaxCapacity, @P_CurrentRegistered, @P_ClassDate, @P_StartTime, @P_EndTime);
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[CRE_INVOICE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_INVOICE_PR]
    @P_UserId INT,
    @P_DiscountId INT,
    @P_Amount DECIMAL(18, 0),
    @P_AmountAfterDiscount DECIMAL(18, 0),
    @P_PaymentMethod NVARCHAR(50),
    @P_IsConfirmed NVARCHAR(50),
    @P_Created DATETIME
AS
BEGIN
    INSERT INTO dbo.invoice_tbl 
        (user_id, discount_id, amount, amount_after_discount, payment_method, is_confirmed, created)
    VALUES 
        (@P_UserId, @P_DiscountId, @P_Amount, @P_AmountAfterDiscount, @P_PaymentMethod, @P_IsConfirmed, @P_Created);
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_MEASURES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_MEASURES_PR]
    @P_ClientId INT,
    @P_Weight DECIMAL(18, 0),
    @P_Height DECIMAL(18, 0),
    @P_AverageOfFat DECIMAL(18, 0)

AS
    INSERT INTO measures_tbl(client_id, weight, height, average_of_fat, created)
    VALUES(@P_ClientId, @P_Weight, @P_Height, @P_AverageOfFat, GETDATE());
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[CRE_MEETINGS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_MEETINGS_PR]
		
    @P_ClientId INT,
    @P_EmployeeId INT,
    @P_TimeOfEntry TIME(7),
    @P_TimeOfExit TIME(7),
    @P_ProgrammedDate DATETIME,
    @P_IsCancelled NVARCHAR(50)
    
AS
BEGIN
    INSERT INTO mettings_tbl(client_id, employee_id, time_of_entry, time_of_exit, programmed_date, is_cancelled, created)
    VALUES(@P_ClientId, @P_EmployeeId, @P_TimeOfEntry, @P_TimeOfExit, @P_ProgrammedDate, @P_IsCancelled, GETDATE());
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Stored Procedure to Create a Membership
CREATE PROCEDURE [dbo].[CRE_MEMBERSHIP_PR]
    @P_Type NVARCHAR(50),
    @P_AmountClassesAllowed INT,
    @P_MonthlyCost FLOAT
AS
BEGIN
    INSERT INTO membership_tbl (type, amount_classes_allowed, monthly_cost, created)
    VALUES (@P_Type, @P_AmountClassesAllowed, @P_MonthlyCost, GETDATE());

    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_OTP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_OTP_PR]
    @P_OTP int,
    @P_UserId int
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si ya existe un OTP no usado para el usuario en los últimos 30 minutos
    IF NOT EXISTS (
        SELECT 1
        FROM otp_tbl
        WHERE user_id = @P_UserId
          AND was_used = 'NO'
          AND created >= DATEADD(MINUTE, -30, GETDATE())
    )
    BEGIN
        -- Insertar el nuevo OTP si no existe uno previo
        INSERT INTO otp_tbl(otp, user_id, expire_date, was_used, created)
        VALUES (@P_OTP, @P_UserId, DATEADD(MINUTE, 30, GETDATE()), 'NO', GETDATE());
    END
    ELSE
    BEGIN
        -- Manejar el caso donde ya existe un OTP válido (opcional)
        -- Por ejemplo, podría devolver un mensaje de error o actualizar el OTP existente
        RAISERROR('Ya existe un OTP válido para este usuario.',16,1) ;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[CRE_PASSWORD_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[CRE_PASSWORD_PR]
    @P_UserId INT,
    @P_PasswordContent NVARCHAR(500)
AS
BEGIN
    INSERT INTO password_tbl (user_id, password, created)
    VALUES (@P_UserId, @P_PasswordContent, GETDATE());
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_PERSONAL_TRAINING_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_PERSONAL_TRAINING_PR]
    @P_ClientId INT,
    @P_EmployeeId INT,
    @P_IsCancelled NVARCHAR(50),
    @P_IsPaid NVARCHAR(50),
    @P_TimeOfEntry TIME(7),
    @P_TimeOfExit TIME(7),
    @P_ProgrammedDate DATETIME,
    @P_HourlyRate DECIMAL(18, 0)
AS
BEGIN
    INSERT INTO dbo.personal_training_tbl (client_id, employee_id, is_cancelled, is_paid, time_of_entry, time_of_exit, programed_date, hourly_rate)
    VALUES (@P_ClientId, @P_EmployeeId, @P_IsCancelled, @P_IsPaid, @P_TimeOfEntry, @P_TimeOfExit, @P_ProgrammedDate, @P_HourlyRate);
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_ROL_PR]
	@P_Name nvarchar(50)
AS
	INSERT INTO rol_tbl(name)
	VALUES(@P_Name)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[CRE_ROUTINE_EXERCISE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CRE_ROUTINE_EXERCISE_PR]
	@P_RoutineId INT,
	@P_ExerciseId INT
AS
BEGIN
    INSERT INTO routine_exercise_tbl(exercise_id, routine_id)
    VALUES (@P_ExerciseId, @P_RoutineId);
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_ROUTINE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CRE_ROUTINE_PR]
	@P_ClientId INT,
	@P_Name VARCHAR(50)
AS
BEGIN
    INSERT INTO routine_tbl(client_id, created, name)
    VALUES (@P_ClientId, GETDATE(), @P_Name);
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_ROUTINE_PROGRESS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRE_ROUTINE_PROGRESS_PR]
	@P_RoutineId INT,
	@P_ExerciseId INT,
    @P_Sets INT,
	@P_Weight INT,
	@P_Reps INT,
    @P_Duration INT,
	@P_Comments VARCHAR(200)
AS
BEGIN
    INSERT INTO [routine_progression_tbl] (routine_id, exercise_id, sets, weight, reps, duration, comments, created)
    VALUES (@P_RoutineId, @P_ExerciseId, @P_Sets, @P_Weight, @P_Reps, @P_Duration, @P_Comments, GETDATE());
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_SCHEDULE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRE_SCHEDULE_PR]
    @P_EmployeeId INT,
    @P_DaysOfWeek NVARCHAR(50),
    @P_TimeOfEntry TIME(7),
    @P_TimeOfExit TIME(7)
AS
BEGIN
    INSERT INTO schedule_tbl(employee_id, days_of_week, time_of_entry, time_of_exit, created)
    VALUES(@P_EmployeeId, @P_DaysOfWeek, @P_TimeOfEntry, @P_TimeOfExit, GETDATE());
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_USER_GROUP_CLASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_USER_GROUP_CLASS_PR]
    @P_GroupClassId INT,
    @P_ClientId INT
AS
BEGIN
    INSERT INTO user_group_class_tbl(group_class_id, client_id, created)
    VALUES(@P_GroupClassId, @P_ClientId, GETDATE());
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_USER_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRE_USER_MEMBERSHIP_PR]

    @P_UserId INT,
    @P_MembershipId INT
AS
BEGIN
    INSERT INTO [dbo].[user_membership_tbl] (user_id, membership_id, created)
    VALUES (@P_UserId, @P_MembershipId, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[CRE_USER_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRE_USER_PR]
    @P_Name NVARCHAR(50),
    @P_LastName NVARCHAR(50),
    @P_Phone NVARCHAR(50),
    @P_Email NVARCHAR(50),
    @P_Status NVARCHAR(50),
    @P_Gender NVARCHAR(50),
    @P_BirthDate DATETIME
AS
BEGIN
    INSERT INTO user_tbl (name, last_name, phone, email, last_login, status, gender, birthdate, created)
    VALUES (@P_Name, @P_LastName, @P_Phone, @P_Email, GETDATE(), @P_Status, @P_Gender, @P_BirthDate, GETDATE());
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[CRE_USER_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRE_USER_ROL_PR]
	@P_User_ID int,
	@P_Rol_ID int
AS
BEGIN
	INSERT INTO user_rol_tbl(user_id,rol_id)
	VALUES(@P_User_ID,@P_Rol_ID)
RETURN 0
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_BYIDUSER_SCHEDULE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_BYIDUSER_SCHEDULE_PR]
    @P_IdUser INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.schedule_tbl WHERE employee_id = @P_IdUser)
    BEGIN
        DELETE FROM dbo.schedule_tbl
        WHERE employee_id = @P_IdUser;
        RETURN 0; -- Success
    END
    ELSE
    BEGIN
        RETURN 1; -- Failure
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_DETAIL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_DETAIL_PR]
    @P_Id INT
AS
BEGIN
    DELETE FROM dbo.detail_tbl
    WHERE id = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_DISCOUNT_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Delete a Discount
CREATE PROCEDURE [dbo].[DEL_DISCOUNT_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe discount
    IF EXISTS (SELECT 1 FROM dbo.discount_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.discount_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success
    END
    RETURN 1; -- Failure: Discount not found
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_EQUIPMENT_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_EQUIPMENT_PR]
	@P_Id int
AS
BEGIN
    -- Verificar que existe product
    IF EXISTS (SELECT 1 FROM dbo.equipment_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.equipment_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Nice
    END
    ELSE
    BEGIN
        RETURN 1; -- Nicen't
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_EXE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Stored Procedure to Delete User
CREATE PROCEDURE [dbo].[DEL_EXE_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe user
    IF EXISTS (SELECT 1 FROM dbo.exercise_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.exercise_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success
    END
    RETURN 1; -- Failure: User not found
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_GROUP_CLASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_GROUP_CLASS_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe group class
    IF EXISTS (SELECT 1 FROM dbo.group_class_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.group_class_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Nice
    END
    ELSE
    BEGIN
        RETURN 1; -- Nicen't
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_INVOICE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_INVOICE_PR]
    @P_Id INT
AS
BEGIN
    DELETE FROM dbo.invoice_tbl
    WHERE id = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_MEASURES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_MEASURES_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe la medida
    IF EXISTS (SELECT 1 FROM dbo.measures_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.measures_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Éxito
    END
    ELSE
    BEGIN
        RETURN 1; -- No se encontró la medida
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_MEETINGS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_MEETINGS_PR]
    @P_Id INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.mettings_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.mettings_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success
    END
    ELSE
    BEGIN
        RETURN 1; -- Failure
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Delete a Membership
CREATE PROCEDURE [dbo].[DEL_MEMBERSHIP_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe membership
    IF EXISTS (SELECT 1 FROM dbo.membership_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.membership_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success
    END
    RETURN 1; -- Failure: Membership not found
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_PERSONAL_TRAINING_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_PERSONAL_TRAINING_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe el registro
    IF EXISTS (SELECT 1 FROM dbo.personal_training_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.personal_training_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Eliminado exitosamente
    END
    ELSE
    BEGIN
        RETURN 1; -- Registro no encontrado
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_ROL_PR]
	@P_IDUsuario int,
	@P_IDRol int
AS
	Delete FROM user_rol_tbl 
	where @P_IDUsuario = user_id and @P_IDRol = rol_id;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[DEL_SCHEDULE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_SCHEDULE_PR]
    @P_Id INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.schedule_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.schedule_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success
    END
    ELSE
    BEGIN
        RETURN 1; -- Failure
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_USER_GROUP_CLASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_USER_GROUP_CLASS_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe user group class
    IF EXISTS (SELECT 1 FROM dbo.user_group_class_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.user_group_class_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success
    END
    ELSE
    BEGIN
        RETURN 1; -- Failure
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_USER_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_USER_MEMBERSHIP_PR]
 
    @P_Id INT
AS
BEGIN
    DELETE FROM [dbo].[user_membership_tbl]
    WHERE id = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[DEL_USER_PASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DEL_USER_PASS_PR]
    @P_Id INT
AS
BEGIN
	DELETE FROM password_tbl
	WHERE user_id = @P_Id
RETURN 0
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_USER_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Delete User
CREATE PROCEDURE [dbo].[DEL_USER_PR]
    @P_Id INT
AS
BEGIN
    -- Verificar que existe user
    IF EXISTS (SELECT 1 FROM dbo.user_tbl WHERE id = @P_Id)
    BEGIN
        DELETE FROM dbo.user_tbl
        WHERE id = @P_Id;
        RETURN 0; -- Success

		DELETE FROM dbo.user_rol_tbl
        WHERE user_id = @P_Id;
        RETURN 0; -- Success
    END
    RETURN 1; -- Failure: User not found
END;
GO
/****** Object:  StoredProcedure [dbo].[DEL_USER_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_USER_ROL_PR]
    @P_Id INT
AS
BEGIN
	DELETE FROM user_rol_tbl
	WHERE user_id = @P_Id
RETURN 0
END;
GO
/****** Object:  StoredProcedure [dbo].[RE_LAST_5_PASSWORDS_BY_ID]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RE_LAST_5_PASSWORDS_BY_ID]
	@P_IdUser int
AS
BEGIN
    SELECT TOP 5 *
    FROM password_tbl
    WHERE user_id = @P_IdUser
    ORDER BY created DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_DETAILS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_DETAILS_PR]
AS
BEGIN
    SELECT 
        id,
        invoice_id,
        user_membership_id,
        personal_training_id,
        price,
        created
    FROM dbo.detail_tbl;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_DISCOUNTS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Retrieve All Discounts
CREATE PROCEDURE [dbo].[RET_ALL_DISCOUNTS_PR]
AS
BEGIN
    SELECT id, type, coupon, percentage, valid_from, valid_to, created
    FROM discount_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_EQUIPMENTS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_EQUIPMENTS_PR]
AS
	SELECT id,name,description,location,created
	FROM equipment_tbl;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_EXE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Stored Procedure to Retrieve All Users
CREATE PROCEDURE [dbo].[RET_ALL_EXE_PR]
AS
BEGIN
    SELECT ex.[id]
		,ex.[name]
		,ex.[type]
		,ex.[equipment_id]
		,eq.name as equipmentName
		,ex.[sets]
		,ex.[reps]
		,ex.[weight]
		,ex.[duration]
    FROM exercise_tbl ex
	INNER JOIN equipment_tbl eq on eq.id = ex.equipment_id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_GROUP_CLASSES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_GROUP_CLASSES_PR]
AS
    SELECT id, employee_id, class_name, max_capacity, current_registered, class_time, start_time, end_time
    FROM group_class_tbl;
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_GROUP_CLASSES_W_NAME_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_GROUP_CLASSES_W_NAME_PR]
AS
BEGIN
    SELECT 
        gc.id,
        gc.class_name,
        gc.max_capacity,
        gc.current_registered,
        gc.class_time,
        gc.start_time,
        gc.end_time,
		gc.employee_id,
        CONCAT(u.name, ' ', u.last_name) AS full_name
    FROM 
        dbo.group_class_tbl gc
    JOIN 
        dbo.user_tbl u
    ON 
        gc.employee_id = u.id;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_INVOICES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_INVOICES_PR]
AS
BEGIN
    SELECT * 
    FROM dbo.invoice_tbl;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_INVOICES_W_DETAILS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_INVOICES_W_DETAILS_PR]
AS
BEGIN
    SELECT 
        i.id,
        i.user_id,
        u.name + ' ' + u.last_name AS full_name,
        i.discount_id,
        i.amount,
        i.amount_after_discount,
        i.payment_method,
        i.is_confirmed,
        i.created
    FROM 
        dbo.invoice_tbl i
    INNER JOIN 
        dbo.user_tbl u ON i.user_id = u.id;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_MEASURES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_MEASURES_PR]
AS
    SELECT id, client_id, weight, height, average_of_fat, created
    FROM measures_tbl;
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_MEETINGS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_MEETINGS_PR]
AS
BEGIN
    SELECT id, client_id, employee_id, time_of_entry, time_of_exit, programmed_date, is_cancelled, created
    FROM mettings_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_MEETINGS_W_NAMES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_MEETINGS_W_NAMES_PR]
AS
BEGIN
    SELECT 
        m.id,
        m.client_id,
        CONCAT(c.name, ' ', c.last_name) AS client_name,
        m.employee_id,
        CONCAT(e.name, ' ', e.last_name) AS employee_name,
        m.time_of_entry,
        m.time_of_exit,
        m.programmed_date,
        m.is_cancelled,
        m.created
    FROM 
        dbo.mettings_tbl m
    LEFT JOIN 
        dbo.user_tbl c ON m.client_id = c.id
    LEFT JOIN 
        dbo.user_tbl e ON m.employee_id = e.id
END
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_MEMBERSHIPS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Retrieve All Memberships
CREATE PROCEDURE [dbo].[RET_ALL_MEMBERSHIPS_PR]
AS
BEGIN
    SELECT id, type, amount_classes_allowed, monthly_cost, created
    FROM membership_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_PERSONAL_TRAININGS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_PERSONAL_TRAININGS_PR]
AS
BEGIN
    SELECT id, client_id, employee_id, is_cancelled, is_paid, time_of_entry, time_of_exit, programed_date, hourly_rate
    FROM dbo.personal_training_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_ROLES_BY_IDUSER_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_ROLES_BY_IDUSER_PR]
    @P_ID_User INT
AS
BEGIN
    SELECT r.id, r.name
    FROM rol_tbl r
    INNER JOIN user_rol_tbl ur ON r.id = ur.rol_id
    WHERE ur.user_id = @P_ID_User;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_ROLES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_ROLES_PR]
AS
	SELECT id, name
    FROM rol_tbl;
    RETURN 0;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_SCHEDULES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_SCHEDULES_PR]
AS
BEGIN
    SELECT id, employee_id, days_of_week, time_of_entry, time_of_exit, created
    FROM schedule_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_USER_GROUP_CLASSES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_USER_GROUP_CLASSES_PR]
AS
BEGIN
    SELECT id, group_class_id, client_id, created
    FROM user_group_class_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_USER_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_USER_MEMBERSHIP_PR]
AS
BEGIN
    SELECT id, user_id, membership_id, created
    FROM [dbo].[user_membership_tbl];
END
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_USER_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_USER_ROL_PR]
AS
	SELECT id,user_id,rol_id FROM 
	user_rol_tbl
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_ALL_USERS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Retrieve All Users
CREATE PROCEDURE [dbo].[RET_ALL_USERS_PR]
AS
BEGIN
    SELECT id, name, last_name, phone, email, last_login, status, gender, birthdate, created
    FROM user_tbl;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_AVA_GROUP_CLASSES_W_NAME_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[RET_AVA_GROUP_CLASSES_W_NAME_PR]
AS
BEGIN
    SELECT 
        gc.id,
        gc.class_name,
        gc.max_capacity,
        gc.current_registered,
        gc.class_time,
        gc.start_time,
        gc.end_time,
        gc.employee_id,
        CONCAT(u.name, ' ', u.last_name) AS full_name
    FROM 
        dbo.group_class_tbl gc
    JOIN 
        dbo.user_tbl u
    ON 
        gc.employee_id = u.id
    WHERE 
        DATEADD(SECOND, DATEDIFF(SECOND, '00:00:00', gc.start_time), CAST(gc.class_time AS DATETIME)) > GETDATE();
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_DETAIL_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_DETAIL_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT 
        id,
        invoice_id,
        user_membership_id,
        personal_training_id,
        price,
        created
    FROM dbo.detail_tbl
    WHERE id = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_DISCOUNT_BYID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Retrieve a Discount by ID
CREATE PROCEDURE [dbo].[RET_DISCOUNT_BYID_PR]
    @P_ID INT
AS
BEGIN
    SELECT id, type, coupon, percentage, valid_from, valid_to, created
    FROM discount_tbl
    WHERE id = @P_ID;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_EQUIPMENT_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_EQUIPMENT_BY_ID_PR]
	@P_Id INT
AS
	SELECT id,name,location,description,created
	FROM equipment_tbl
	WHERE id = @P_Id;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_EXE_BY_ROUTINE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_EXE_BY_ROUTINE_PR]
	@P_RoutineId INT
AS
BEGIN
	SELECT ex.[id]
		,ex.[name]
		,ex.[type]
		,ex.[equipment_id]
		,eq.name as equipmentName
		,ex.[sets]
		,ex.[reps]
		,ex.[weight]
		,ex.[duration]
	FROM exercise_tbl ex
	JOIN routine_exercise_tbl re ON (re.exercise_id = ex.id)
	INNER JOIN equipment_tbl eq ON (eq.id = ex.equipment_id)
	WHERE re.routine_id = @P_RoutineId
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_GROUP_CLASS_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_GROUP_CLASS_BY_ID_PR]
    @P_Id INT
AS
    SELECT id, employee_id, class_name, max_capacity, current_registered, class_time, start_time, end_time
    FROM group_class_tbl
    WHERE id = @P_Id;
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[RET_GROUP_CLASSES_BYUSERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_GROUP_CLASSES_BYUSERID_PR]
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [id],
        [employee_id],
        [class_name],
        [max_capacity],
        [current_registered],
        [class_time],
        [start_time],
        [end_time]
    FROM 
        [dbo].[group_class_tbl]
    WHERE 
        [employee_id] = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_INVOICE_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_INVOICE_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT * 
    FROM dbo.invoice_tbl
    WHERE id = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_LAST_ROUTINE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RET_LAST_ROUTINE_PR]
AS
BEGIN
    SELECT * FROM routine_tbl ORDER BY created DESC;
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_LATEST_INVOICE_BY_USERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_LATEST_INVOICE_BY_USERID_PR]
    @P_UserId INT
AS
BEGIN
    SELECT TOP 1
        id,
        user_id,
        discount_id,
        amount,
        amount_after_discount,
        payment_method,
        is_confirmed,
        created
    FROM dbo.invoice_tbl
    WHERE user_id = @P_UserId
    ORDER BY created DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_MEASURES_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_MEASURES_BY_ID_PR]
    @P_Id INT
AS
    SELECT id, client_id, weight, height, average_of_fat, created
    FROM measures_tbl
    WHERE id = @P_Id;
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[RET_MEASURES_BY_USERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_MEASURES_BY_USERID_PR]
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [id],
        [client_id],
        [weight],
        [height],
        [average_of_fat],
        [created]
    FROM 
        [dbo].[measures_tbl]
    WHERE 
        [client_id] = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_MEETING_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_MEETING_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT id, client_id, employee_id, time_of_entry, time_of_exit, programmed_date, is_cancelled, created
    FROM mettings_tbl
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_MEETING_BYUSERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_MEETING_BYUSERID_PR]
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [id],
        [client_id],
        [employee_id],
        [time_of_entry],
        [time_of_exit],
        [programmed_date],
        [is_cancelled],
        [created]
    FROM 
        [dbo].[mettings_tbl]
    WHERE 
        [employee_id] = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_MEMBERSHIP_BYID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Retrieve a Membership by ID
CREATE PROCEDURE [dbo].[RET_MEMBERSHIP_BYID_PR]
    @P_ID INT
AS
BEGIN
    SELECT id, type, amount_classes_allowed, monthly_cost, created
    FROM membership_tbl
    WHERE id = @P_ID;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_PERSONAL_TRAINING_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_PERSONAL_TRAINING_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT id, client_id, employee_id, is_cancelled, is_paid, time_of_entry, time_of_exit, programed_date, hourly_rate
    FROM dbo.personal_training_tbl
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_PERSONAL_TRAINING_BYUSERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_PERSONAL_TRAINING_BYUSERID_PR]
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [id],
        [client_id],
        [employee_id],
        [is_cancelled],
        [is_paid],
        [time_of_entry],
        [time_of_exit],
        [programed_date],
        [hourly_rate]
    FROM 
        [dbo].[personal_training_tbl]
    WHERE 
        [employee_id] = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_PT_BY_CLIENTID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_PT_BY_CLIENTID_PR]
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pt.[id],
        pt.[client_id],
        pt.[employee_id],
        pt.[is_cancelled],
        pt.[is_paid],
        pt.[time_of_entry],
        pt.[time_of_exit],
        pt.[programed_date],
        pt.[hourly_rate],
        CONCAT(emp.[name], ' ', emp.[last_name]) AS employee_full_name,
        CONCAT(cli.[name], ' ', cli.[last_name]) AS client_full_name
    FROM 
        [dbo].[personal_training_tbl] pt
    LEFT JOIN 
        [dbo].[user_tbl] emp ON pt.[employee_id] = emp.[id]
    LEFT JOIN 
        [dbo].[user_tbl] cli ON pt.[client_id] = cli.[id]
    WHERE 
        pt.[client_id] = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_PT_BY_EMPLOYEEID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_PT_BY_EMPLOYEEID_PR]
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pt.[id],
        pt.[client_id],
        pt.[employee_id],
        pt.[is_cancelled],
        pt.[is_paid],
        pt.[time_of_entry],
        pt.[time_of_exit],
        pt.[programed_date],
        pt.[hourly_rate],
        CONCAT(emp.[name], ' ', emp.[last_name]) AS employee_full_name,
        CONCAT(cli.[name], ' ', cli.[last_name]) AS client_full_name
    FROM 
        [dbo].[personal_training_tbl] pt
    LEFT JOIN 
        [dbo].[user_tbl] emp ON pt.[employee_id] = emp.[id]
    LEFT JOIN 
        [dbo].[user_tbl] cli ON pt.[client_id] = cli.[id]
    WHERE 
        pt.[employee_id] = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_ROL_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ROL_BY_ID_PR]
	@P_ID INT
AS
	SELECT * FROM rol_tbl
	where id = @P_ID;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_ROUTINE_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_ROUTINE_BY_ID_PR]
	@P_Id INT
AS
BEGIN
    SELECT *
	FROM routine_tbl
	WHERE id = @P_Id
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ROUTINE_BY_USER_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[RET_ROUTINE_BY_USER_PR]
	@P_ClientId INT
AS
BEGIN
    SELECT * FROM routine_tbl WHERE client_id = @P_ClientId
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_ROUTINE_PROGRESSION_BY_ROUTINE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[RET_ROUTINE_PROGRESSION_BY_ROUTINE_PR]
	@P_RoutineId INT
AS
BEGIN
    SELECT rp.[id]
      ,rp.[routine_id]
      ,rp.[exercise_id]
	  ,e.[name] AS exercise_name
      ,rp.[sets]
      ,rp.[weight]
      ,rp.[reps]
      ,rp.[duration]
      ,rp.[comments]
      ,rp.[created]
	FROM [dbo].[routine_progression_tbl] rp 
	JOIN [dbo].[exercise_tbl] e ON (e.id = rp.exercise_id)
	WHERE rp.[routine_id] = @P_RoutineId
    
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_SCHEDULE_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_SCHEDULE_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT id, employee_id, days_of_week, time_of_entry, time_of_exit, created
    FROM schedule_tbl
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_SCHEDULE_BYUSERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_SCHEDULE_BYUSERID_PR]
    @P_EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.id,
        s.employee_id,
        s.days_of_week,
        s.time_of_entry,
        s.time_of_exit,
        s.created
    FROM 
        dbo.schedule_tbl s
    WHERE 
        s.employee_id = @P_EmployeeId;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_BY_CREDENTIALS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_USER_BY_CREDENTIALS_PR]
	@P_Email varchar(50),
	@P_Password varchar(50)
AS
SELECT 
	us.id, us.name, us.last_name, us.phone, us.email, us.last_login, us.status, us.gender, us.birthdate, us.created
FROM user_tbl us
INNER JOIN password_tbl ps on ps.user_id = us.id
WHERE 
	us.Email = @P_Email
	AND ps.password = @P_Password
    AND ps.created = (
        SELECT MAX(created) 
        FROM password_tbl 
        WHERE user_id = us.id
);

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_BY_EMAIL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_BY_EMAIL_PR]
	@Email_US NVARCHAR(50)
AS
Begin
	Select id From user_tbl 
	where email = @Email_US;
End
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_BYEMAIL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_USER_BYEMAIL_PR]
	@P_Email nvarchar(50)
AS
	SELECT * FROM user_tbl
	where email = @P_Email;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_BYID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_BYID_PR]
	@P_ID INT
AS
	SELECT * FROM user_tbl
	where id = @P_ID;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_BYROLE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_BYROLE_PR]
    @P_ROLE_ID INT
AS
BEGIN
    SELECT 
        u.[id],
        u.[name],
        u.[last_name],
        u.[phone],
        u.[email],
        u.[last_login],
        u.[status],
        u.[gender],
        u.[birthdate],
        u.[created]
    FROM 
        [dbo].[user_tbl] u
    INNER JOIN 
        [dbo].[user_rol_tbl] ur ON u.id = ur.user_id
    WHERE 
        ur.rol_id = @P_ROLE_ID;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_BYROLE_W_SCHED_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_BYROLE_W_SCHED_PR]
    @P_ROLE_ID INT
AS
BEGIN
    SELECT 
        u.[id],
        u.[name],
        u.[last_name],
        u.[phone],
        u.[email],
        u.[last_login],
        u.[status],
        u.[gender],
        u.[birthdate],
        u.[created],
        s.[days_of_week],
        s.[time_of_entry],
        s.[time_of_exit]
    FROM 
        [dbo].[user_tbl] u
    INNER JOIN 
        [dbo].[user_rol_tbl] ur ON u.id = ur.user_id
    INNER JOIN 
        [dbo].[schedule_tbl] s ON u.id = s.employee_id
    WHERE 
        ur.rol_id = @P_ROLE_ID;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_GROUP_CLASS_BY_GROUP_CLASS_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_GROUP_CLASS_BY_GROUP_CLASS_ID_PR]
    @P_GP_Id INT
AS
BEGIN
    SELECT 
        [id],
        [group_class_id],
        [client_id],
        [created]
    FROM 
        [dbo].[user_group_class_tbl]
    WHERE 
        [group_class_id] = @P_GP_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_GROUP_CLASS_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_GROUP_CLASS_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT id, group_class_id, client_id, created
    FROM user_group_class_tbl
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_GROUP_CLASS_WITH_NAME_BY_GROUP_CLASS_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_GROUP_CLASS_WITH_NAME_BY_GROUP_CLASS_ID_PR]
    @P_GP_Id INT
AS
BEGIN
    SELECT 
        ugc.id,
        ugc.group_class_id,
        ugc.client_id,
        ugc.created,
        u.name + ' ' + u.last_name AS client_name
    FROM 
        [dbo].[user_group_class_tbl] ugc
    INNER JOIN 
        [dbo].[user_tbl] u ON ugc.client_id = u.id
    WHERE 
        ugc.group_class_id = @P_GP_Id
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_MEMBERSHIP_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_MEMBERSHIP_BY_ID_PR]
    @P_Id INT
AS
BEGIN
    SELECT id, user_id, membership_id, created
    FROM [dbo].[user_membership_tbl]
    WHERE id = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_MEMBERSHIP_BY_USERID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_MEMBERSHIP_BY_USERID_PR]
    @P_UserId INT
AS
BEGIN
    SELECT id, user_id, membership_id, created
    FROM [dbo].[user_membership_tbl]
    WHERE user_id = @P_UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_MEMBERSHIP_BY_USERID_STATUS_V_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_MEMBERSHIP_BY_USERID_STATUS_V_PR]
    @P_UserId INT
AS
BEGIN
    DECLARE @CurrentDate DATE = GETDATE();
    DECLARE @CreatedDate DATE;
    DECLARE @MembershipID INT;

    -- Verificar si el usuario tiene el rol con rol_id = 4
    IF NOT EXISTS (
        SELECT 1
        FROM [dbo].[user_rol_tbl]
        WHERE user_id = @P_UserId AND rol_id = 4
    )
    BEGIN
        -- Si el usuario no tiene el rol, salir sin realizar más acciones
        RETURN;
    END

    -- Obtener la fecha de creación y el ID de la membresía
    SELECT @CreatedDate = created, @MembershipID = membership_id
    FROM [dbo].[user_membership_tbl]
    WHERE user_id = @P_UserId;

    -- Verificar si la fecha de creación sobrepasa un mes
    IF DATEDIFF(MONTH, @CreatedDate, @CurrentDate) > 1
    BEGIN
        -- Actualizar el estado del usuario a 'Expired'
        UPDATE [dbo].[user_tbl]
        SET status = 'Expired'
        WHERE id = @P_UserId;

        -- Devolver un error indicando que la membresía ha expirado
        RAISERROR('Su membresía ha acabado.', 16, 1);
        RETURN;
    END

    -- Si la membresía no ha expirado, devolver los datos de la membresía
    SELECT id, user_id, membership_id, created
    FROM [dbo].[user_membership_tbl]
    WHERE user_id = @P_UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[RET_USER_ROL_BY_ID_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_USER_ROL_BY_ID_PR]
	@P_ID INT
AS
	SELECT * FROM user_rol_tbl
	where id = @P_ID;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UPD_CANCEL_MEETING_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_CANCEL_MEETING_PR]
    @P_Id INT
AS
BEGIN
    -- Comenzar una transacción para asegurar la atomicidad
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Actualizar la columna is_cancelled a 'si' donde el id coincide
        UPDATE [dbo].[mettings_tbl]
        SET [is_cancelled] = 'si'
        WHERE [id] = @P_Id;

        -- Confirmar la transacción si todo sale bien
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Revertir la transacción en caso de error
        ROLLBACK TRANSACTION;
        
        -- Manejo del error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanza el error para que pueda ser manejado externamente
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_CANCEL_PERSONAL_TRAINING]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_CANCEL_PERSONAL_TRAINING]
    @P_Id INT
AS
BEGIN
    -- Establecer la opción para manejar errores
    SET NOCOUNT ON;

    -- Actualizar el campo is_cancelled a 'si' para el registro con el id proporcionado
    UPDATE [dbo].[personal_training_tbl]
    SET [is_cancelled] = 'si'
    WHERE [id] = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_DETAIL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_DETAIL_PR]
    @P_Id INT,
    @P_InvoiceId INT,
    @P_UserMembershipId INT = NULL,
    @P_PersonalTrainingId INT = NULL,
    @P_Price DECIMAL(18, 0)
AS
BEGIN
    UPDATE dbo.detail_tbl
    SET
        invoice_id = @P_InvoiceId,
        user_membership_id = @P_UserMembershipId,
        personal_training_id = @P_PersonalTrainingId,
        price = @P_Price
    WHERE id = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_DISCOUNT_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Update Discount
CREATE PROCEDURE [dbo].[UPD_DISCOUNT_PR]
    @P_Id INT,
    @P_Type NVARCHAR(50),
    @P_Coupon NVARCHAR(50),
    @P_Percentage INT,
    @P_ValidFrom DATETIME,
    @P_ValidTo DATETIME
AS
BEGIN
    UPDATE dbo.discount_tbl
    SET 
        type = @P_Type,
        coupon = @P_Coupon,
        percentage = @P_Percentage,
        valid_from = @P_ValidFrom,
        valid_to = @P_ValidTo
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_EQUIPMENT_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_EQUIPMENT_PR]
	@P_Id INT,
	@P_Name NVARCHAR(50),
	@P_Description NVARCHAR(200),
	@P_Location NVARCHAR(150)
AS
	UPDATE dbo.equipment_tbl
    SET 
        name = @P_Name,
        description = @P_Description,
        location = @P_Location
    WHERE id = @P_Id;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UPD_EXE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Stored Procedure to Update User
CREATE PROCEDURE [dbo].[UPD_EXE_PR]
    @P_Id INT,
	@P_Name NVARCHAR(50),
    @P_Type NVARCHAR(50),
	@P_EquipmentId INT,
    @P_Sets INT,
	@P_Reps INT,
    @P_Weight INT,
    @P_Duration INT
AS
BEGIN
    UPDATE dbo.exercise_tbl
    SET 
        name = @P_Name,
        type = @P_Type,
        equipment_id = @P_EquipmentId,
        sets = @P_Sets,
        reps = @P_Reps,
        weight = @P_Weight,
        duration = @P_Duration
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_GROUP_CLASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_GROUP_CLASS_PR]
    @P_Id INT,
    @P_EmployeeId INT,
    @P_ClassName NVARCHAR(50),
    @P_MaxCapacity INT,
    @P_CurrentRegistered INT,
    @P_ClassDate DATETIME,
    @P_StartTime TIME(7),
    @P_EndTime TIME(7)
AS
    UPDATE dbo.group_class_tbl
    SET 
        employee_id = @P_EmployeeId,
        class_name = @P_ClassName,
        max_capacity = @P_MaxCapacity,
        current_registered = @P_CurrentRegistered,
        class_time = @P_ClassDate,
        start_time = @P_StartTime,
        end_time = @P_EndTime
    WHERE id = @P_Id;
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[UPD_INVOICE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_INVOICE_PR]
    @P_Id INT,
    @P_UserId INT,
    @P_DiscountId INT,
    @P_Amount DECIMAL(18, 0),
    @P_AmountAfterDiscount DECIMAL(18, 0),
    @P_PaymentMethod NVARCHAR(50),
    @P_IsConfirmed NVARCHAR(50)
AS
BEGIN
    UPDATE dbo.invoice_tbl
    SET 
        user_id = @P_UserId,
        discount_id = @P_DiscountId,
        amount = @P_Amount,
        amount_after_discount = @P_AmountAfterDiscount,
        payment_method = @P_PaymentMethod,
        is_confirmed = @P_IsConfirmed
    WHERE 
        id = @P_Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_MEASURES_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_MEASURES_PR]
    @P_Id INT,
    
    @P_Weight DECIMAL(18, 0),
    @P_Height DECIMAL(18, 0),
    @P_AverageOfFat DECIMAL(18, 0)
   
AS
    UPDATE dbo.measures_tbl
    SET 
                weight = @P_Weight,
        height = @P_Height,
        average_of_fat = @P_AverageOfFat
        
    WHERE id = @P_Id;
RETURN 0;
GO
/****** Object:  StoredProcedure [dbo].[UPD_MEETINGS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_MEETINGS_PR]
    @P_Id INT,
    @P_ClientId INT,
    @P_EmployeeId INT,
    @P_TimeOfEntry TIME(7),
    @P_TimeOfExit TIME(7),
    @P_ProgrammedDate DATETIME,
    @P_IsCancelled NVARCHAR(50),
    @P_Created DATETIME
AS
BEGIN
    UPDATE dbo.mettings_tbl
    SET 
        client_id = @P_ClientId,
        employee_id = @P_EmployeeId,
        time_of_entry = @P_TimeOfEntry,
        time_of_exit = @P_TimeOfExit,
        programmed_date = @P_ProgrammedDate,
        is_cancelled = @P_IsCancelled,
        created = @P_Created
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Stored Procedure to Update Membership
CREATE PROCEDURE [dbo].[UPD_MEMBERSHIP_PR]
    @P_Id INT,
    @P_Type NVARCHAR(50),
    @P_AmountClassesAllowed INT,
    @P_MonthlyCost FLOAT
AS
BEGIN
    UPDATE dbo.membership_tbl
    SET 
        type = @P_Type,
        amount_classes_allowed = @P_AmountClassesAllowed,
        monthly_cost = @P_MonthlyCost
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_OTP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_OTP_PR]
    @User_id int,
    @Email varchar(100),
    @Phone varchar(20),
    @OTP int -- Parámetro para el OTP ingresado por el usuario (tipo int)
AS
BEGIN
    BEGIN TRANSACTION

        -- Validación de existencia de email y teléfono proporcionados
        IF NOT EXISTS (
            SELECT 1 
            FROM user_tbl 
            WHERE id = @User_id AND email = @Email AND phone = @Phone
        )
        BEGIN
            RAISERROR('Los datos proporcionados no coinciden con el usuario.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validación del OTP
        IF NOT EXISTS (
            SELECT 1 
            FROM otp_tbl 
            WHERE user_id = @User_id AND otp = @OTP AND was_used = 'No'
        )
        BEGIN
            RAISERROR('El OTP ingresado es inválido.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Actualización de OTP a usado
        UPDATE otp_tbl
        SET was_used = 'Si'
        WHERE user_id = @User_id AND otp = @OTP;

        -- Verificación del rol del usuario
        DECLARE @RolID int;
        SELECT @RolID = rol_id 
        FROM user_rol_tbl 
        WHERE user_id = @User_id;

        -- Actualización del estado según el rol
        IF @RolID = 4
        BEGIN
            UPDATE user_tbl
            SET status = 'Verified'
            WHERE id = @User_id;
        END
        ELSE
        BEGIN
            UPDATE user_tbl
            SET status = 'Active'
            WHERE id = @User_id;
        END

    COMMIT TRANSACTION
END
GO
/****** Object:  StoredProcedure [dbo].[UPD_PERSONAL_TRAINING_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_PERSONAL_TRAINING_PR]
    @P_Id INT,
    @P_ClientId INT,
    @P_EmployeeId INT,
    @P_IsCancelled NVARCHAR(50),
    @P_IsPaid NVARCHAR(50),
    @P_TimeOfEntry TIME(7),
    @P_TimeOfExit TIME(7),
    @P_ProgrammedDate DATETIME,
    @P_HourlyRate DECIMAL(18, 0)
AS
BEGIN
    UPDATE dbo.personal_training_tbl
    SET 
        client_id = @P_ClientId,
        employee_id = @P_EmployeeId,
        is_cancelled = @P_IsCancelled,
        is_paid = @P_IsPaid,
        time_of_entry = @P_TimeOfEntry,
        time_of_exit = @P_TimeOfExit,
        programed_date = @P_ProgrammedDate,
        hourly_rate = @P_HourlyRate
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_ROL_PR]
	@P_ID INT,
    @P_Name NVARCHAR(50)
AS
BEGIN
    UPDATE rol_tbl
    SET 
		name = @P_Name
    WHERE id = @P_ID;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_SCHEDULE_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_SCHEDULE_PR]
    @P_Id INT,
    @P_EmployeeId INT,
    @P_DaysOfWeek NVARCHAR(50),
    @P_TimeOfEntry TIME(7),
    @P_TimeOfExit TIME(7),
    @P_Created DATETIME
AS
BEGIN
    UPDATE dbo.schedule_tbl
    SET 
        employee_id = @P_EmployeeId,
        days_of_week = @P_DaysOfWeek,
        time_of_entry = @P_TimeOfEntry,
        time_of_exit = @P_TimeOfExit,
        created = @P_Created
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_USER_GROUP_CLASS_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_USER_GROUP_CLASS_PR]
    @P_Id INT,
    @P_GroupClassId INT,
    @P_ClientId INT
AS
BEGIN
    UPDATE dbo.user_group_class_tbl
    SET 
        group_class_id = @P_GroupClassId,
        client_id = @P_ClientId
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_USER_MEMBERSHIP_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UPD_USER_MEMBERSHIP_PR]
    @P_Id INT,
    @P_UserId INT,
    @P_MembershipId INT
AS
BEGIN
    UPDATE [dbo].[user_membership_tbl]
    SET user_id = @P_UserId,
        membership_id = @P_MembershipId,
        created = GETDATE()
    WHERE id = @P_Id;
END
GO
/****** Object:  StoredProcedure [dbo].[UPD_USER_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Stored Procedure to Update User
CREATE PROCEDURE [dbo].[UPD_USER_PR]
    @P_Id INT,
    @P_Name NVARCHAR(50),
    @P_LastName NVARCHAR(50),
    @P_Phone NVARCHAR(50),
    @P_Email NVARCHAR(50),
    @P_LastLogin DATETIME,
    @P_Status NVARCHAR(50),
    @P_Gender NVARCHAR(50),
    @P_BirthDate DATETIME
AS
BEGIN
    UPDATE dbo.user_tbl
    SET 
        name = @P_Name,
        last_name = @P_LastName,
        phone = @P_Phone,
        email = @P_Email,
        last_login = @P_LastLogin,
        status = @P_Status,
        gender = @P_Gender,
        birthdate = @P_BirthDate
    WHERE id = @P_Id;
    RETURN 0;
END;
GO
/****** Object:  StoredProcedure [dbo].[UPD_USER_ROL_PR]    Script Date: 17/8/2024 20:48:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Stored Procedure to Update User
CREATE PROCEDURE [dbo].[UPD_USER_ROL_PR]
	@P_User_ID INT,
    @P_Rol_ID INT
AS
BEGIN
    UPDATE dbo.user_rol_tbl
    SET 
        rol_id = @P_Rol_ID
    WHERE user_id = @P_User_ID;
    RETURN 0;
END;
GO
ALTER DATABASE [GymDB] SET  READ_WRITE 
GO
