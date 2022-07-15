if not exists (select 1 from dbo.[Joke])
begin 
	insert into dbo.[Joke] (JokeQuestion, JokeAnswer)
	values ('If you''re American when you go in the bathroom and American when you come out, what are you in the bathroom?', 'European'),
	('What''s the difference between a rabbit and a plum?', 'They''re both purple except for the rabbit.'),
	('Two guys walk into a bar.' , 'The third guy ducks.'),
	('What’s green and has wheels?', 'Grass. I lied about the wheels.');
end