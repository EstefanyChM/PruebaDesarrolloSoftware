USE [TrabajadoresPrueba]
GO
/****** Object:  StoredProcedure [dbo].[ListarTrabajadores]    Script Date: 13/07/2025 19:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListarTrabajadores]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.Id,
        t.TipoDocumento,
        t.NumeroDocumento,
        t.Nombres,
        t.Sexo,
        d.NombreDepartamento,
        p.NombreProvincia,
        dis.NombreDistrito
    FROM [dbo].[Trabajadores] t
    LEFT JOIN [dbo].[Departamento] d ON d.Id = t.IdDepartamento
    LEFT JOIN [dbo].[Provincia] p ON p.Id = t.IdProvincia
    LEFT JOIN [dbo].[Distrito] dis ON dis.Id = t.IdDistrito
END
GO
/****** Object:  StoredProcedure [dbo].[ListarTrabajadoresConArgumento]    Script Date: 13/07/2025 19:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListarTrabajadoresConArgumento]
    @Sexo NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.Id,
        t.TipoDocumento,
        t.NumeroDocumento,
        t.Nombres,
        t.Sexo,
        d.NombreDepartamento,
        p.NombreProvincia,
        dis.NombreDistrito
    FROM [dbo].[Trabajadores] t
    LEFT JOIN [dbo].[Departamento] d ON d.Id = t.IdDepartamento
    LEFT JOIN [dbo].[Provincia] p ON p.Id = t.IdProvincia
    LEFT JOIN [dbo].[Distrito] dis ON dis.Id = t.IdDistrito
    WHERE (@Sexo IS NULL OR t.Sexo = @Sexo)
END
GO
