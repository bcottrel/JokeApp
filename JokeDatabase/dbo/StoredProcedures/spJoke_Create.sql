CREATE PROCEDURE [dbo].[spJoke_Create]
	@Id int,
	@JokeQuestion nvarchar (MAX),
	@JokeAnswer nvarchar (MAX)
AS
begin
	update dbo.[Joke]
	set JokeQuestion = @JokeQuestion,
		JokeAnswer = @JokeAnswer
	where Id = @Id;
end

