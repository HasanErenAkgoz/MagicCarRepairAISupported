-- Migration: AddFuelTypeToVehicle
-- Date: 2026-03-15
-- Description: Adds FuelType column to Vehicles table

IF NOT EXISTS (
    SELECT 1 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'[dbo].[Vehicles]') 
    AND name = 'FuelType'
)
BEGIN
    ALTER TABLE [Vehicles]
    ADD [FuelType] nvarchar(50) NULL;
    
    PRINT 'FuelType column added to Vehicles table successfully.';
END
ELSE
BEGIN
    PRINT 'FuelType column already exists in Vehicles table.';
END
GO
