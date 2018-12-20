
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/20/2018 15:54:54
-- Generated from EDMX file: D:\OA\MyWebApp.OA\MyWebApp.OA.Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OA];
GO

-- --------------------------------------------------



-- Creating foreign key on [ActionInfo_ID] in table 'ActionInfoDepartment'
ALTER TABLE [dbo].[ActionInfoDepartment]
ADD CONSTRAINT [FK_ActionInfoDepartment_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Department_ID] in table 'ActionInfoDepartment'
ALTER TABLE [dbo].[ActionInfoDepartment]
ADD CONSTRAINT [FK_ActionInfoDepartment_Department]
    FOREIGN KEY ([Department_ID])
    REFERENCES [dbo].[Department]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoDepartment_Department'
CREATE INDEX [IX_FK_ActionInfoDepartment_Department]
ON [dbo].[ActionInfoDepartment]
    ([Department_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------