CREATE PROCEDURE [dbo].[spJoke_Insert]
	@JokeQuestion nvarchar(MAX),
	@JokeAnswer nvarchar(MAX)
AS
begin
	insert
	into dbo.[Joke] (JokeQuestion, JokeAnswer)
	values (@JokeQuestion, @JokeAnswer);
end

