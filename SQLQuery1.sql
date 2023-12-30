select * from ProductTable

Insert into ProductTable Values (NEWID(),'2024 Toyota 4Runner', '$40,656', 'The 2024 Toyota 4Runner is a great off-roader with lots of room to stow your outdoor adventure gear,
																			but it fails to impress beyond those traits. It feels stuck in the past, thanks to a dino-age interior,
																			unrefined on-road driving manners and an overall lack of refinement.', 'C:\Users\BJ\Documents\C# projects\WPF_CarApplication\WPF\Images\Carr1.jpg')

Insert into ProductTable Values (NEWID(),'2024 Toyota Camry', '$26,240', 'The 2024 Toyota Camry is a nicely balanced and comfortable sedan that is sure to please as a daily commuter or family road-tripper.
																		  It is not flawless, but it further establishes the Camrys reputation as a sensible, feature-rich and well-made car.', 'C:\Users\BJ\Documents\C# projects\WPF_CarApplication\WPF\Images\Car2.jpg')

EXEC [GetProductProcedure]

Insert into ProductTable Values (NEWID(),'2034 Toyota Camry', '$26,240', 'The 2024 Toyota Camry is a nicely balanced and comfortable sedan that is sure to please as a daily commuter or family road-tripper.
																		  It is not flawless, but it further establishes the Camrys reputation as a sensible, feature-rich and well-made car.', 'C:\Users\BJ\Documents\C# projects\WPF_CarApplication\WPF\Images\Car2.jpg')

Insert into ProductTable Values (NEWID(),'2000 Toyota 4Runner', '$40,656', 'The 2024 Toyota 4Runner is a great off-roader with lots of room to stow your outdoor adventure gear,
																			but it fails to impress beyond those traits. It feels stuck in the past, thanks to a dino-age interior,
																			unrefined on-road driving manners and an overall lack of refinement.', 'C:\Users\BJ\Documents\C# projects\WPF_CarApplication\WPF\Images\Carr1.jpg')
select * from ProductTable

Declare @ProductId uniqueidentifier = '8D1CC16D-76FC-4C25-8755-011B7A7673F6'

EXEC [GetProductByIdProcedure] @ProductId