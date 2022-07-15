CREATE PROCEDURE [dbo].[spJoke_Get]
	@Id int
AS
begin
	select *
	from dbo.[Joke]
	where Id = @Id;
end

