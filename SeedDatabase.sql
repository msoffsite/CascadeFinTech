DECLARE @JohnDoeId                  UNIQUEIDENTIFIER = '8A812F08-0647-443B-8FA3-A98C3B9493A7',
        @JaneDoeId                  UNIQUEIDENTIFIER = '42441057-b6c1-4852-9ea7-1f382f99e4eb',
        @BeforeTheyAreHangedId      UNIQUEIDENTIFIER = '9BC6B1F8-5A82-4925-BB38-4C0890B25C67',
        @CarrieId                   UNIQUEIDENTIFIER = '2B333A66-9D78-4967-AF79-2D64CE6F4479',
        @ChristineId                UNIQUEIDENTIFIER = 'C36B2014-3A5E-46F7-B83F-0E23453E1363',
        @LastArgumentOfKingsId      UNIQUEIDENTIFIER = '21F82CB7-4328-4A9D-9420-6EEBE08E5E37',
        @TheBladeItselfId           UNIQUEIDENTIFIER = 'E1158088-C85A-4217-BC04-4CD71F9F275B',
        @TheShiningId               UNIQUEIDENTIFIER = '0E679291-2961-4415-B9B9-115EC15874EF',
        @StephenKingId              UNIQUEIDENTIFIER = 'F40B159A-53FE-406C-8102-90100F4236F4',
        @JoeAbercrombieId           UNIQUEIDENTIFIER = '242480CF-3CC8-4A0D-90FE-F3A99EA994ED',
        @RandomHouseId              UNIQUEIDENTIFIER = '084030A4-F0CA-4D9C-B567-1A5B259E7F87',
        @TorId                      UNIQUEIDENTIFIER = '268AFD6D-9D14-42D4-88F0-5C632760D941';

INSERT INTO [orders].[Customer]
    VALUES
    (@JohnDoeId, 'johndoe@mail.com', 'John Doe', 1),
    (@JaneDoeId, 'janedoe@mail.com', 'Jane Doe', 1);

INSERT INTO [dbo].[Author]
    VALUES
    (@StephenKingId, 'Stephen', 'King'),
    (@JoeAbercrombieId, 'Joe', 'Abercrombie');

INSERT INTO [dbo].[Publisher]
    VALUES
    (@RandomHouseId, 'Random House'),
    (@TorId, 'Tor');

INSERT INTO [dbo].[Book]
    VALUES
    (@BeforeTheyAreHangedId, @JoeAbercrombieId, @TorId, 'Before They Are Hanged'),
    (@TheBladeItselfId, @JoeAbercrombieId, @TorId, 'The Blade Itself'),
    (@LastArgumentOfKingsId, @JoeAbercrombieId, @TorId, 'Last Argument of Kings'),
    (@CarrieId, @StephenKingId, @RandomHouseId, 'Carrie'),
    (@ChristineId, @StephenKingId, @RandomHouseId, 'Christine'),
    (@TheShiningId, @StephenKingId, @RandomHouseId, 'The Shining');

INSERT INTO [orders].[BookPrice]
    VALUES
    (@BeforeTheyAreHangedId, 20, 'USD'),
    (@BeforeTheyAreHangedId, 18, 'EUR'),
    (@TheBladeItselfId, 21, 'USD'),
    (@TheBladeItselfId, 19, 'EUR'),
    (@LastArgumentOfKingsId, 22, 'USD'),
    (@LastArgumentOfKingsId, 20, 'EUR'),
    (@CarrieId, 23, 'USD'),
    (@CarrieId, 21, 'EUR'),
    (@ChristineId, 24, 'USD'),
    (@ChristineId, 22, 'EUR'),
    (@TheShiningId, 25, 'USD'),
    (@TheShiningId, 23, 'EUR');