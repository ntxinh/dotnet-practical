/****** Object:  Table [dbo].[Foo]    Script Date: 13/11/2019 10:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Foos]') AND type IN ('U'))
	DROP TABLE [dbo].[Foos]
GO

CREATE TABLE [dbo].[Foos](
    [Id] int  IDENTITY(1,1) NOT NULL,
	[Bar] [varchar](100) NOT NULL,
	[FooBar] [varchar](100) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] INT NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[UpdatedBy] INT NOT NULL,
	[IsDeleted] BIT NOT NULL,
 CONSTRAINT [PK_Foo] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
