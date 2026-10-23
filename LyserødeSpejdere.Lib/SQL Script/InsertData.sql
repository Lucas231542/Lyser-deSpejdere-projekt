-- Køres lige efter CreateTables.sql, så id´erne starter på 1

-- Brugere: 2 ledere (ErLeder = 1) og 4 spejdere (ErLeder = 0)

INSERT INTO Bruger (Navn, Mail, Brugernavn, Adgangskode, Foedselsdato, ErLeder) VALUES 
                                                                                    
('Anton', 'Anton@spejder.dk', 'Anton', 'leder123', '1994-03-10', 1), -- id 1
('Aleks', 'Aleks@spejder.dk', 'Aleks', 'leder123', '1995-04-11', 1), -- id 2
('Sofyane', 'Sofyane@spejder.dk', 'Sofyane', 'spejder123', '1988-07-12', 0), -- id 3
('Lucas', 'Lucas@spejder.dk', 'Lucas', 'spejder123', '1998-05-08', 0), -- id 4
('Ahmed', 'Ahmed@spejder.dk', 'Ahmed', 'spejder123', '1988-04-11', 0), -- id 5
('Khadija', 'Khadija@spejder.dk', 'Khadija', 'spejder123', '2017-01-20', 0); -- id 6

-- Tegn/færdigheder 
INSERT INTO Faerdighed (Navn) VALUES        -- Faerdighed har kun en kolonne, vi udfylder navn. id'et kommer automatisk.
('Førstehjælp'), -- id 1    
('Ridemærke'),   -- id 2 
('Bålmærke');    -- id 3

-- Aktivitetstyper med regler (NULL = Ingen grænse)      -- Hvad betyder dette. Ridning max 4 spejder, mindst 10 år (Khadija 9 år må altså ikke komme med)
                 -- Mad over bål : NULL i begge, så der er INGEN GRÆNSER. Alle må deltage. Det viser casens "Nogle aktiviteter har begrænsninger".
                 -- Fang dyr: Max 6, mindst 8 år.   NULL skrives uden '  '  fordi det betyder "ingen værdi" og ikke teksten "NULL"
                 
INSERT INTO AktivitetType (Navn, Beskrivelse, MaxDeltagere, MinAlder) VALUES    
('Ridning',  'Besøg på gården og ridetur', 4, 10), -- id 1
('Mad over Bål', 'Vi laver aftensmad over bål', NULL, NULL), -- id 2
('Fang dyr ved vandhul', 'Tur til vandhullet med net', 6, 8); -- id 3

-- Aktiviteter: Konkrete gange (Format: 'ÅÅÅÅ-MM-DDTtt:mm:ss')
                                                              
-- Det første tal er AktivitetTypeId (FK). 1 = Ridning, 2 = Mad over bål, 3 = fang dyr.
-- Ridning findes to gange ( Mandag og onsdag) Det viser forskellen på HVAD (AktivitetType) OG HVORNÅR (AKTIVITET).
                 -- T mellem dato og sted er SQL servers sikret format. Uden T kan SQL server på nogle computer bytte rundt på måed og dag

INSERT INTO Aktivitet(AktivitetTypeId, StartTid, SlutTid, Sted) VALUES 
(1, '2026-07-06T10:00:00', '2026-07-06T12:00:00', 'Gården'),        -- id 1: Ridning Mandag
(1, '2026-07-08T14:00:00', '2026-07-08T16:00:00', 'Gården'),        -- id 2: Ridning Onsdag
(2, '2026-07-06T18:00:00', '2026-07-06T20:00:00', 'Lejrpladsen'),   -- id 3: Mad over bål
(3, '2026-07-07T10:00:00', '2026-07-07T12:00:00', 'Vandhullet');    -- id 4: Fang dyr

-- Ledere: Anton (1) og Aleks (2)
                                    -- Hver række = en forbindelse: (Hvilken leder, hvilken aktivitet).
                                    -- Mad over bål har to ledere (Række 3 og 4). Det viser casens krav "en til flere ledere"
                                    -- Alle aktiviteter har mindst en leder. 
INSERT INTO AktivitetLeder (BrugerId, AktivitetId) VALUES 
(1,1), -- Anton Leder ridning Mandag
(1,2), -- Anton leder ridning Onsdag
(1,3), -- Anton leder mad over bål
(2,3), -- Aleks leder også mad over bål
(2,4); -- Aleks leder Fang dyr

-- Hvilke tegn spejderne har: (spejder, tegn)
       
INSERT INTO BrugerFaerdighed (BrugerId, FaerdighedId) VALUES 
(3,1), -- Sofyane har førstehjælp
(3,2), -- Sofyane har ridemærke
(5,2), -- Ahmed har ridemærke 
(4,3); -- Lucas har Bålmærke

-- Hvilke tegn en aktivitetstype kræver: (Type, tegn)

INSERT INTO AktivitetTypeKrav (AktivitetTypeId, FaerdighedId) VALUES
(1, 2); -- Ridning kræver Ridemærke

-- Tilmeldinger: (spejder, aktivitet) - alle overholder reglerne
        
                            -- HVorfor de her data: Det viser alle casens regler:
                            -- Ridning har kun spejdere med ridemærke over 10 år.
                            -- Khadija (9 år) Kan ikke komme med til ridning, men gere til fang dyr (Grænsen er 8).
                            -- Lucas har ikke ridemærke, så han kan ikke komme med til ridning.
                            -- Mad over bål har ingen grænser, så alle kan deltage.

INSERT INTO Tilmelding (BrugerId, AktivitetId) VALUES 
(3,1),  -- Sofyane: ridning mandag (Har ridemærke, over 10 år)
(5,1),  -- Ahmed: ridning mandag (Har ridemærke over 10 år)
(3,3),  -- Sofyane: Mad over bål
(4,3),  -- Lucas: Mad over bål
(6,3),  -- Khadija: Mad over bål (Ingen grænser)
(4,4),  -- Lucas: Fang dyr
(6,4);  -- Khadija: fang dyr (9 år, grænsen er 8)





