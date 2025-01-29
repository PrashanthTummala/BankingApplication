USE [BankingDB]
GO

/****** Object:  Table [dbo].[Transactions]    Script Date: 29-01-2025 15:49:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[FromAccount] [int] NULL,
	[ToAccount] [int] NULL,
	[TransactionTime] [datetime] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[FromAccountBalance] [decimal](18, 2) NOT NULL,
	[ToAccountBalance] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transactions] ADD  DEFAULT (getdate()) FOR [TransactionTime]
GO

ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_FromAccount] FOREIGN KEY([FromAccount])
REFERENCES [dbo].[Accounts] ([AccountId])
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_FromAccount]
GO

ALTER TABLE [dbo].[Transactions]  WITH NOCHECK ADD CHECK  (([Amount]>(0)))
GO


