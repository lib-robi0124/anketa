-- Create ReportRoles table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ReportRoles')
BEGIN
    CREATE TABLE [ReportRoles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [CanViewAll] bit NOT NULL,
        [CanViewSubordinate] bit NOT NULL,
        [CanViewSpecificOU] bit NOT NULL,
        CONSTRAINT [PK_ReportRoles] PRIMARY KEY ([Id])
    );
    
    PRINT 'ReportRoles table created successfully';
END
ELSE
BEGIN
    PRINT 'ReportRoles table already exists';
END
GO

-- Create ReportUsers table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ReportUsers')
BEGIN
    CREATE TABLE [ReportUsers] (
        [Id] int NOT NULL IDENTITY,
        [LevCompanyId] int NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [ReportRoleId] int NOT NULL,
        [AllowedOUs] nvarchar(max) NOT NULL,
        [AllowedOU2s] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ReportUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReportUsers_ReportRoles_ReportRoleId] FOREIGN KEY ([ReportRoleId]) 
            REFERENCES [ReportRoles] ([Id]) ON DELETE NO ACTION
    );
    
    CREATE INDEX [IX_ReportUsers_ReportRoleId] ON [ReportUsers] ([ReportRoleId]);
    
    PRINT 'ReportUsers table created successfully';
END
ELSE
BEGIN
    PRINT 'ReportUsers table already exists';
END
GO

-- Seed ReportRoles data
IF NOT EXISTS (SELECT * FROM ReportRoles WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT [ReportRoles] ON;
    
    INSERT INTO [ReportRoles] ([Id], [Name], [Description], [CanViewAll], [CanViewSubordinate], [CanViewSpecificOU])
    VALUES 
        (1, 'Admin', 'Full access to all reports', 1, 0, 0),
        (2, 'OUManager', 'Access to subordinate level reports', 0, 1, 0),
        (3, 'Viewer', 'Access to specific OU reports only', 0, 0, 1);
    
    SET IDENTITY_INSERT [ReportRoles] OFF;
    
    PRINT 'ReportRoles data seeded successfully';
END
ELSE
BEGIN
    PRINT 'ReportRoles data already exists';
END
GO

-- Seed ReportUsers data
IF NOT EXISTS (SELECT * FROM ReportUsers WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT [ReportUsers] ON;
    
    INSERT INTO [ReportUsers] ([Id], [LevCompanyId], [Password], [ReportRoleId], [AllowedOUs], [AllowedOU2s], [CreatedDate], [IsActive])
    VALUES 
        (1, 4420621, '20621', 1, 'Production,Projects and IT,Supply chain,HR', 'Rolling Unit,Coating Unit,Human Resources,Quality control,Planning & Strategy', '2025-01-01', 1),
        (2, 4420975, '20975', 3, 'Production', 'Rolling Unit,Coating Unit', '2025-01-01', 1);
    
    SET IDENTITY_INSERT [ReportUsers] OFF;
    
    PRINT 'ReportUsers data seeded successfully';
END
ELSE
BEGIN
    PRINT 'ReportUsers data already exists';
END
GO

PRINT 'Script completed successfully!';
