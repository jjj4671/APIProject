CREATE DATABASE SoccerStats;
USE SoccerStats;

CREATE TABLE Players(
	TeamNo INT NOT NULL UNIQUE,
	PlayerName VARCHAR(20) NOT NULL,
	Goals INT NOT NULL,
	Assists INT NOT NULL,
	Top10 VARCHAR(20),
	PRIMARY KEY (TeamNo),
	FOREIGN KEY (PlayerName)
);

CREATE TABLE Teams(
	TeamNo INT NOT NULL UNIQUE,
    TablePos INT NOT NULL,
    LastYearPos Int NOT NULL,
    UCLQualify VARCHAR(16) NOT NULL,
    PRIMARY KEY (TeamNo),
    
);


    
INSERT INTO PlayerStats (TeamNo, PlayerName, Goals, Assists, Top10) VALUES
	(1, "Benzema", 10, 5, "yes"),
	(2, "Araujo", 5, 1, "yes");

INSERT INTO TeamStats (TeamNo, TablePos, LastYearPos, UCLQualify) VALUES 
	(1, 1, 2, "yes"),
	(2, 5, 1, "no");
        
    