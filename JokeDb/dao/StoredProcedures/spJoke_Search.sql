CREATE PROCEDURE [dbo].[spJoke_Search]
	@JokeQuestion nvarchar(max),
	@JokeAnswer nvarchar(max)
AS
	SELECT * 
	FROM dbo.Joke 
	WHERE JokeQuestion LIKE 
	@JokeQuestion OR 
	JokeAnswer LIKE @JokeAnswer
RETURN 0
