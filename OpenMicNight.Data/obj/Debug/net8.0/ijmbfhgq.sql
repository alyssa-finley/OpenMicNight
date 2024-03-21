IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Performer] (
    [PerformerId] int NOT NULL IDENTITY,
    [PerformerType] nvarchar(max) NOT NULL,
    [PerformerName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Performer] PRIMARY KEY ([PerformerId])
);
GO

CREATE TABLE [Music] (
    [MusicId] int NOT NULL IDENTITY,
    [PerformerId] int NOT NULL,
    [PerformerName] nvarchar(max) NOT NULL,
    [Datetime] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Music] PRIMARY KEY ([MusicId]),
    CONSTRAINT [FK_Music_Performer_PerformerId] FOREIGN KEY ([PerformerId]) REFERENCES [Performer] ([PerformerId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Song] (
    [SongId] int NOT NULL IDENTITY,
    [PerformerId] int NOT NULL,
    [MusicId] int NOT NULL,
    [SongName] nvarchar(max) NOT NULL,
    [IsOriginal] bit NOT NULL,
    CONSTRAINT [PK_Song] PRIMARY KEY ([SongId]),
    CONSTRAINT [FK_Song_Music_MusicId] FOREIGN KEY ([MusicId]) REFERENCES [Music] ([MusicId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Song_Performer_PerformerId] FOREIGN KEY ([PerformerId]) REFERENCES [Performer] ([PerformerId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Music_PerformerId] ON [Music] ([PerformerId]);
GO

CREATE INDEX [IX_Song_MusicId] ON [Song] ([MusicId]);
GO

CREATE INDEX [IX_Song_PerformerId] ON [Song] ([PerformerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240314185018_init', N'8.0.3');
GO

COMMIT;
GO

