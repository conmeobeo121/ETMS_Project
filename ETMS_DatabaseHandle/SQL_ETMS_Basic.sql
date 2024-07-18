-- Không liên kết với file database, liên kết trực tiếp với SQL Server.
-- Mục đích: Tách riêng 2 giao diện VS và SSMS để dễ quản lý và thao tác.
-- Vui lòng copy toàn bộ đoạn code này vào SQL Server để chạy, hoặc, nếu chạy trong file, bỏ đoạn code được đánh dấu
-- Sau khi chèn các bảng và dữ liệu liên quan, lấy chuỗi kết nối và set nó vào trong file Config.cs để khởi chạy.

-- CÓ THỂ BỎ ĐƯỢC
USE master
GO
ALTER DATABASE [QuanLyBanVeSuKien] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
DROP DATABASE QuanLyBanVeSuKien
GO

CREATE DATABASE QuanLyBanVeSuKien
GO

USE QuanLyBanVeSuKien
GO
--


CREATE TABLE Users 
(
    [UserID] INT PRIMARY KEY IDENTITY(1,1),
    [Username] NVARCHAR(50) UNIQUE NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(512) NOT NULL,
    [UserType] NVARCHAR(20) NOT NULL CHECK (UserType IN (N'Admin', N'Customer'))
)
GO

CREATE TRIGGER trgHashPassword
ON Users
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted EXCEPT SELECT * FROM deleted)
    BEGIN				
        -- Xử lý chèn mới
        INSERT INTO Users (Username, Email, Password, UserType)
        SELECT 
            i.Username, 
            i.Email, 
            LOWER(CONVERT(VARCHAR(512), HASHBYTES('SHA2_256', i.Password), 2)), 
            i.UserType
        FROM inserted i;
    END
    ELSE IF EXISTS (SELECT * FROM deleted)
    BEGIN
        -- Xử lý cập nhật
        UPDATE Users
        SET 
            Username = i.Username,
            Password = LOWER(CONVERT(VARCHAR(512), HASHBYTES('SHA2_256', i.Password), 2)), 
            Email = i.Email, 
            UserType = i.UserType
        FROM inserted i
        WHERE Users.UserID = i.UserID;
    END
END
GO


CREATE TABLE [Venues] 
(
    [VenueID] INT IDENTITY(1,1) PRIMARY KEY,
    [VenueName] NVARCHAR(255) NOT NULL UNIQUE,
    [VenueAddress] NVARCHAR(500) NOT NULL,
    [VenueCapacity] INT NOT NULL CHECK ([VenueCapacity] > 0),
    [VenueCity] NVARCHAR(255) NOT NULL,
    [VenueState] NVARCHAR(255) NOT NULL,
    [VenueZipCode] NVARCHAR(10) NOT NULL
)
GO


CREATE TABLE EventTypes 
(
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(50) UNIQUE NOT NULL
)
GO

CREATE TABLE [Events]
(
	EventID INT PRIMARY KEY IDENTITY(1, 1),
	EventName NVARCHAR(255) UNIQUE NOT NULL,
	EventDescription NTEXT,
	EventStartDate DATETIME NOT NULL DEFAULT GETDATE(),
	EventEndDate DATETIME NOT NULL DEFAULT DATEADD(DAY, 7, GETDATE()),
	VenueID INT NOT NULL FOREIGN KEY REFERENCES Venues(VenueID),
	TypeID INT NOT NULL FOREIGN KEY REFERENCES [EventTypes]([TypeID])
)
GO

ALTER TABLE [Events]
ADD CONSTRAINT CK_STARTDATE_ENDDATE CHECK (EventStartDate <= EventEndDate)
GO

CREATE TABLE [ImagesEvent]
(
	[ImageID] INT PRIMARY KEY IDENTITY(1, 1),
	[ImageUrl] NVARCHAR(255) NOT NULL,
	[EventID] INT NOT NULL FOREIGN KEY REFERENCES [Events]([EventID])
)
GO

CREATE TABLE [TicketTypes]
(
	[TypeID] INT PRIMARY KEY IDENTITY(1, 1),
	[TypeName] NVARCHAR(255) NOT NULL,	
	[Price] INT NOT NULL CHECK ([Price] > 0),		
	[StartSell] DATETIME NOT NULL,
	[EndSell] DATETIME NOT NULL,
	[EventID] INT NOT NULL FOREIGN KEY REFERENCES [Events]([EventID]),
	[HasSeat] BIT NOT NULL DEFAULT 0,	
	UNIQUE ([TypeName], [EventID])
)
GO

ALTER TABLE [TicketTypes]
ADD CONSTRAINT [CK_StartDate_EndDate_Sell] CHECK ([StartSell] < [EndSell])
GO


CREATE TABLE [PaymentMethods] 
(
    [MethodID] INT PRIMARY KEY IDENTITY(1, 1),
    [MethodName] NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE [Orders] 
(
    [OrderID] INT PRIMARY KEY IDENTITY(1, 1),
    [UserID] INT NOT NULL,
    [PaymentDate] DATE NOT NULL DEFAULT GETDATE(),
    [TotalPrice] BIGINT NOT NULL CHECK ([TotalPrice] > 0),	
	[Status] VARCHAR(20) NOT NULL CHECK ([Status] IN ('PAID', 'CANCELLED')) DEFAULT 'PAID',
    FOREIGN KEY (UserID) REFERENCES [Users]([UserID])
)
GO

CREATE TABLE [Payments] 
(
    [PaymentID] INT PRIMARY KEY IDENTITY(1, 1),
    [OrderID] INT NOT NULL UNIQUE,
    [MethodID] INT NOT NULL,
    [TransactionId] NVARCHAR(50) NOT NULL,
	[Amount] BIGINT NOT NULL CHECK ([Amount] > 0),	
	FOREIGN KEY ([MethodID]) REFERENCES [PaymentMethods]([MethodID]),
    FOREIGN KEY (OrderID) REFERENCES [Orders](OrderID)
)
GO

CREATE TABLE [Tickets]
(
	[TicketID] INT PRIMARY KEY IDENTITY(1, 1),
	[OrderID] INT NOT NULL FOREIGN KEY REFERENCES [Orders]([OrderID]),
	[TypeID] INT NOT NULL FOREIGN KEY REFERENCES [TicketTypes]([TypeID]),
	[TicketCode] VARCHAR(255) NOT NULL UNIQUE,
	[SeatNumber] INT
)
GO

CREATE TRIGGER trg_UpdateTicketTypes
ON [TicketTypes]
AFTER UPDATE
AS
BEGIN
    -- Chỉ thực hiện khi cột HasSeat thay đổi từ 1 về 0
    IF UPDATE(HasSeat)
    BEGIN
        -- Xóa SeatNumber của tất cả các vé có TicketType thay đổi từ HasSeat = 1 sang HasSeat = 0
        UPDATE [Tickets]
        SET [SeatNumber] = NULL
        WHERE [TypeID] IN (
            SELECT i.[TypeID]
            FROM inserted i
            JOIN deleted d ON i.[TypeID] = d.[TypeID]
            WHERE d.[HasSeat] = 1 AND i.[HasSeat] = 0
        );
    END
END
GO


CREATE TABLE [Promotions]
(
	[PromotionID] INT PRIMARY KEY IDENTITY(1, 1),
	[PromotionCode] VARCHAR(50) NOT NULL UNIQUE,
	[DiscountPer] FLOAT NOT NULL CHECK ([DiscountPer] > 0 AND [DiscountPer] <= 100),
	[PromotionStartDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[PromotionEndDate] DATETIME NOT NULL DEFAULT DATEADD(DAY, 7, GETDATE())
)
GO

CREATE TABLE [SpecificPromotions]
(
	[SpecificID] INT PRIMARY KEY IDENTITY(1, 1),
	[PromotionID] INT NOT NULL FOREIGN KEY REFERENCES [Promotions]([PromotionID]),
	[EventID] INT NOT NULL FOREIGN KEY REFERENCES [Events]([EventID]),
	UNIQUE ([PromotionID], [EventID])
)
GO

-- Nếu đoạn code chỗ này bị lỗi, vui lòng bỏ (việc tạo index chỉ giúp truy vấn nhanh hơn)
CREATE NONCLUSTERED INDEX IDX_Users_Email ON Users (Email);
CREATE NONCLUSTERED INDEX IDX_Users_UserType ON Users (UserType);

CREATE NONCLUSTERED INDEX IDX_Venues_VenueCity ON Venues (VenueCity);
CREATE NONCLUSTERED INDEX IDX_Venues_VenueState ON Venues (VenueState);

CREATE NONCLUSTERED INDEX IDX_Events_VenueID ON Events (VenueID);
CREATE NONCLUSTERED INDEX IDX_Events_TypeID ON Events (TypeID);
CREATE NONCLUSTERED INDEX IDX_Events_EventStartDate ON Events (EventStartDate);
CREATE NONCLUSTERED INDEX IDX_Events_EventEndDate ON Events (EventEndDate);

CREATE NONCLUSTERED INDEX IDX_TicketTypes_EventID ON TicketTypes (EventID);

CREATE NONCLUSTERED INDEX IDX_ImageEvents_EventID ON ImageEvents (EventID);

CREATE NONCLUSTERED INDEX IDX_SpecificPromotions_PromotionID ON SpecificPromotions (PromotionID);
CREATE NONCLUSTERED INDEX IDX_SpecificPromotions_EventID ON SpecificPromotions (EventID);
-- Bỏ tới đây

-- Chèn tạm 2 tài khoản Admin vào để thao tác dữ liệu, còn nếu không thì không thể đăng ký trên giao diện được.
INSERT INTO [Users]([Username], [Email], [Password], [UserType])
VALUES (N'ntdll1234', N'aspdotnet@gmail.com', N'Henshin1001', N'Admin')
GO

INSERT INTO [Users]([Username], [Email], [Password], [UserType])
VALUES (N'paperpepper0011', N'holyshjt0011@gmail.com', N'Ariga10233', N'Admin')
GO