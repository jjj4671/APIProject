USE SoccerStats;

CREATE TABLE Teams(
	TeamNo INT NOT NULL UNIQUE,
    TablePos INT NOT NULL,
    LastYearPos Int NOT NULL,
    UCLQualify VARCHAR(16) NOT NULL,
    PRIMARY KEY (TeamNo)
    
);




CREATE TABLE Players(
	TeamNo INT NOT NULL UNIQUE,
	PlayerName VARCHAR(20) NOT NULL,
	Goals INT NOT NULL,
	Assists INT NOT NULL,
	Top10 VARCHAR(20),
	PRIMARY KEY (PlayerName),
	FOREIGN KEY (TeamNo) REFERENCES Teams(TeamNo)
);


INSERT INTO Teams (TeamNo, TablePos, LastYearPos, UCLQualify) VALUES 
	(1, 1, 2, "yes"),
	(2, 5, 1, "no");
    
INSERT INTO Players (TeamNo, PlayerName, Goals, Assists, Top10) VALUES
	(1, "Benzema", 10, 5, "yes"),
	(2, "Araujo", 5, 1, "yes");

        
    