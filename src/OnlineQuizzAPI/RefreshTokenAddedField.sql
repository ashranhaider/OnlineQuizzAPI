BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260122060418_RefreshTokenRenameField'
)
BEGIN
    EXEC sp_rename N'[RefreshTokens].[Token]', N'HashedToken', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260122060418_RefreshTokenRenameField'
)
BEGIN
    EXEC sp_rename N'[RefreshTokens].[IX_RefreshTokens_Token]', N'IX_RefreshTokens_HashedToken', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260122060418_RefreshTokenRenameField'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260122060418_RefreshTokenRenameField', N'9.0.3');
END;

COMMIT;
GO

