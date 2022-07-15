CREATE PROCEDURE [dbo].[spJoke_GetAll]
AS
begin
	select *
	from dbo.[Joke];
end
