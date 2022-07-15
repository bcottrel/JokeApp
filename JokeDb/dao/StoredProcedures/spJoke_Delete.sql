CREATE PROCEDURE [dbo].[spJoke_Delete]
	@Id int
AS
begin
	delete
	from dbo.[Joke]
	where Id = @Id;
end

