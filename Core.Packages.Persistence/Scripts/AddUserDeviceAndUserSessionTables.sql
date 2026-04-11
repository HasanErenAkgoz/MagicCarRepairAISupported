-- Migration: AddUserDeviceAndUserSessionTables
-- Tarih: 2024-03-08
-- Açıklama: Remember Me özelliği için UserDevice ve UserSession tablolarını oluşturur

-- UserDevices Tablosu
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDevices]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[UserDevices] (
        [Id] int NOT NULL IDENTITY(1,1),
        [UserId] int NOT NULL,
        [DeviceId] nvarchar(255) NOT NULL,
        [DeviceName] nvarchar(255) NULL,
        [IsTrusted] bit NOT NULL DEFAULT 0,
        [LastLoginAt] datetime2 NOT NULL,
        [ClientId] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        [CreatedBy] int NOT NULL,
        [ModifiedBy] int NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_UserDevices] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserDevices_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserDevices_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_UserDevices_ClientId] ON [dbo].[UserDevices] ([ClientId]);
    CREATE INDEX [IX_UserDevices_DeviceId] ON [dbo].[UserDevices] ([DeviceId]);
    CREATE INDEX [IX_UserDevices_UserId] ON [dbo].[UserDevices] ([UserId]);
    CREATE UNIQUE INDEX [IX_UserDevices_UserId_DeviceId] ON [dbo].[UserDevices] ([UserId], [DeviceId]);
    
    PRINT 'UserDevices table created successfully.';
END
ELSE
BEGIN
    PRINT 'UserDevices table already exists.';
END
GO

-- UserSessions Tablosu
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSessions]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[UserSessions] (
        [Id] int NOT NULL IDENTITY(1,1),
        [UserId] int NOT NULL,
        [TokenId] nvarchar(255) NOT NULL,
        [DeviceId] nvarchar(255) NULL,
        [DeviceName] nvarchar(255) NULL,
        [IpAddress] nvarchar(45) NULL,
        [UserAgent] nvarchar(500) NULL,
        [IsRemembered] bit NOT NULL DEFAULT 0,
        [ExpiresAt] datetime2 NOT NULL,
        [LastActivityAt] datetime2 NOT NULL,
        [ClientId] int NOT NULL,
        [CreatedDate] datetime2 NULL,
        [ModifiedDate] datetime2 NULL,
        [CreatedBy] int NOT NULL,
        [ModifiedBy] int NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_UserSessions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserSessions_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserSessions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_UserSessions_ClientId] ON [dbo].[UserSessions] ([ClientId]);
    CREATE INDEX [IX_UserSessions_DeviceId] ON [dbo].[UserSessions] ([DeviceId]);
    CREATE INDEX [IX_UserSessions_ExpiresAt] ON [dbo].[UserSessions] ([ExpiresAt]);
    CREATE UNIQUE INDEX [IX_UserSessions_TokenId] ON [dbo].[UserSessions] ([TokenId]);
    CREATE INDEX [IX_UserSessions_UserId] ON [dbo].[UserSessions] ([UserId]);
    
    PRINT 'UserSessions table created successfully.';
END
ELSE
BEGIN
    PRINT 'UserSessions table already exists.';
END
GO

-- Migration History'ye ekle
IF NOT EXISTS (SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = '20260308010000_AddUserDeviceAndUserSessionTables')
BEGIN
    INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20260308010000_AddUserDeviceAndUserSessionTables', '9.0.2');
    PRINT 'Migration history updated.';
END
ELSE
BEGIN
    PRINT 'Migration already recorded in history.';
END
GO

PRINT 'Migration completed successfully!';
