USE [TrabajadoresPrueba]
GO
/****** Object:  StoredProcedure [dbo].[ListarTrabajadores]    Script Date: 12/07/2025 22:45:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ListarTrabajadores]
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
