USE [DataCall]
GO
/****** Object:  Table [dbo].[Feed]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feed](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[feed_text] [varchar](max) NOT NULL,
	[like_quantity] [int] NULL,
	[created_at] [datetime] NOT NULL,
	[comment] [varchar](max) NULL,
	[comment_quantity] [int] NULL,
 CONSTRAINT [PK_Feed] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Age] [int] NULL,
	[Address] [varchar](100) NULL,
	[Phone_Number] [bigint] NULL,
	[Email] [varchar](100) NULL,
	[Account] [varchar](50) NOT NULL,
	[Password] [varbinary](max) NOT NULL,
	[Data_inclusao] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSession]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSession](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[logged_user] [char](1) NOT NULL,
	[login_start] [datetime] NULL,
	[logout_start] [datetime] NULL,
 CONSTRAINT [PK_UserSession] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Feed] ON 

INSERT [dbo].[Feed] ([id], [user_id], [feed_text], [like_quantity], [created_at], [comment], [comment_quantity]) VALUES (1, 4, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', 3, CAST(N'2023-11-12T00:00:00.000' AS DateTime), N'I Like your post', 0)
INSERT [dbo].[Feed] ([id], [user_id], [feed_text], [like_quantity], [created_at], [comment], [comment_quantity]) VALUES (2, 4, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', 3, CAST(N'2023-11-12T00:00:00.000' AS DateTime), N'I Like your post', 0)
INSERT [dbo].[Feed] ([id], [user_id], [feed_text], [like_quantity], [created_at], [comment], [comment_quantity]) VALUES (3, 4, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', 3, CAST(N'2023-11-12T00:00:00.000' AS DateTime), N'I Like your post', 0)
INSERT [dbo].[Feed] ([id], [user_id], [feed_text], [like_quantity], [created_at], [comment], [comment_quantity]) VALUES (4, 4, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', 3, CAST(N'2023-11-12T00:00:00.000' AS DateTime), N'I Like your post', 0)
INSERT [dbo].[Feed] ([id], [user_id], [feed_text], [like_quantity], [created_at], [comment], [comment_quantity]) VALUES (5, 4, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', 3, CAST(N'2023-11-12T00:00:00.000' AS DateTime), N'I Like your post', 0)
SET IDENTITY_INSERT [dbo].[Feed] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Name], [Age], [Address], [Phone_Number], [Email], [Account], [Password], [Data_inclusao]) VALUES (4, N'Rafael Carvalho', NULL, NULL, NULL, NULL, N'rafael.carvalho@danysoft.net', 0x8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92, CAST(N'2023-12-05T22:11:39.360' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserSession] ON 

INSERT [dbo].[UserSession] ([id], [user_id], [logged_user], [login_start], [logout_start]) VALUES (59, 4, N'S', CAST(N'2023-12-11T23:13:08.850' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[UserSession] OFF
GO
ALTER TABLE [dbo].[Feed]  WITH CHECK ADD  CONSTRAINT [FK_Feed_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Feed] CHECK CONSTRAINT [FK_Feed_Users]
GO
ALTER TABLE [dbo].[UserSession]  WITH CHECK ADD  CONSTRAINT [FK_UserSession_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserSession] CHECK CONSTRAINT [FK_UserSession_User]
GO
/****** Object:  StoredProcedure [dbo].[proc_add_user]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE           PROCEDURE [dbo].[proc_add_user]
	@p_id integer,
	@p_name character varying(50),
	@p_account character varying(50),
	@p_password character varying(50)
AS
BEGIN

	BEGIN
		IF @p_name IS NULL OR trim(@p_name) = ''
			THROW 99999, 'O parâmetro @p_name é obrigatório!', 1;
	END

	BEGIN
		IF @p_account IS NULL OR trim(@p_account) = ''
			THROW 99999, 'O parâmetro @p_user é obrigatório!', 1;
	END

	BEGIN
		IF @p_password IS NULL OR trim(@p_password) = ''
			THROW 99999, 'O parâmetro @p_password é obrigatório!', 1;
	END

	BEGIN
		IF  (select count(*) from users where Account = @p_account) > 0
			THROW 99999, 'Usuário já cadastrado no sistema!', 1;
	END

	DECLARE @hashed_password VARBINARY(256) = HASHBYTES('SHA2_256', @p_password)

	BEGIN
		IF @p_id IS NULL OR @p_id <= 0

			DECLARE @id INT;
			INSERT INTO users (Name, Account, Password, Data_inclusao) VALUES (@p_name, @p_account, @hashed_password, GETDATE());

			SET @id = SCOPE_IDENTITY();

			INSERT INTO UserSession (user_id, logged_user) VALUES (@id, 'N');
		--ELSE
		--	BEGIN
		--		IF NOT EXISTS (SELECT * FROM users WHERE id = @p_id)
		--			THROW 99999, 'O código informado não foi encontrado!', 1;
		--		ELSE
		--			UPDATE users SET 
		--				Name = @p_name ,
		--				Account = @p_account,
		--				Password = @p_password
		--			WHERE id = @p_id;
		--	END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[proc_home_get]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--exec [dbo].[proc_home_get]
--@p_id = 3

CREATE     PROCEDURE [dbo].[proc_home_get]
	@p_id int
AS
BEGIN

	BEGIN
		IF @p_id IS NULL OR @p_id = ''
			THROW 99999, 'O parâmetro @p_id é obrigatório!', 1;
	END

	BEGIN
		SELECT 
		u.Name as user_name
		,f.*
		FROM
		Feed f
		inner join Users u on u.ID = @p_id
		WHERE
		user_id = @p_id

	END
END
GO
/****** Object:  StoredProcedure [dbo].[proc_login]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





--Consulta se o usuário existe na tabela
CREATE             PROCEDURE [dbo].[proc_login]
	--@p_id integer,
	--@p_name character varying(50),
	@p_account character varying(50),
	@p_password character varying(50)
AS
BEGIN
	BEGIN
		IF @p_account IS NULL OR trim(@p_account) = ''
			THROW 99999, 'O parâmetro @p_user é obrigatório!', 1;
	END

	BEGIN
		IF @p_password IS NULL OR trim(@p_password) = ''
			THROW 99999, 'O parâmetro @p_password é obrigatório!', 1;
	END

	DECLARE @hashed_password VARBINARY(256) = HASHBYTES('SHA2_256', @p_password)

	BEGIN

		SELECT 
		u.Id 
		FROM USERS u 
		INNER JOIN UserSession us ON u.Id = us.user_id --AND us.logged_user = 'N'		 
		ORDER BY u.Id DESC

		UPDATE 
		UserSession 
		SET logged_user = 'S', login_start = GETDATE(), logout_start = NULL
		WHERE (user_id = (select Id from Users)) 

	END
END
GO
/****** Object:  StoredProcedure [dbo].[proc_logout]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






--Consulta se o usuário existe na tabela
CREATE   PROCEDURE [dbo].[proc_logout]
	@p_user_id integer,
	@p_logged_user CHAR(1) = 'N'
AS
BEGIN
	BEGIN
		IF @p_user_id IS NULL OR @p_user_id = 0
			THROW 99999, 'O parâmetro @p_id é obrigatório!', 1;
	END

	BEGIN
		UPDATE UserSession
		SET logout_start = GETDATE(), logged_user = @p_logged_user
		WHERE user_id = @p_user_id AND logged_user = 'S';
	END
END
GO
/****** Object:  StoredProcedure [dbo].[proc_users]    Script Date: 12/11/2023 11:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE       PROCEDURE [dbo].[proc_users]
	@p_id integer,
	@p_name character varying(50),
	@p_account character varying(50),
	@p_password character varying(50)
AS
BEGIN
	BEGIN
		IF @p_name IS NULL OR trim(@p_name) = ''
			THROW 99999, 'O parâmetro @p_name é obrigatório!', 1;
	END

	BEGIN
		IF @p_account IS NULL OR trim(@p_account) = ''
			THROW 99999, 'O parâmetro @p_user é obrigatório!', 1;
	END

	BEGIN
		IF @p_password IS NULL OR trim(@p_password) = ''
			THROW 99999, 'O parâmetro @p_password é obrigatório!', 1;
	END

	BEGIN
		IF @p_id IS NULL OR @p_id <= 0
			INSERT INTO users (Name, Account, Password) VALUES (@p_name, @p_account, @p_password);
		--ELSE
		--	BEGIN
		--		IF NOT EXISTS (SELECT * FROM users WHERE id = @p_id)
		--			THROW 99999, 'O código informado não foi encontrado!', 1;
		--		ELSE
		--			UPDATE users SET 
		--				Name = @p_name ,
		--				Account = @p_account,
		--				Password = @p_password
		--			WHERE id = @p_id;
		--	END
	END
END
GO
