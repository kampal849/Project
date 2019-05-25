using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType
{
    Tetris,
    Race, 
    MG, 
    BB, 
    SN, 
    SS
}

public enum Themes
{
    Space,
    Ocean, 
    Retro
}

public enum Settings
{
    audio,
    music,
    theme
}

public static class Vault
{
    //TODO: więcej opcji i poprawić działanie klasy, żeby była prywatna
    public class Options
    {
        public float audioVolume, musicVolume;
        public Themes graphicTheme;

        Options(bool load)
        {
            if(load)
            {
                audioVolume = PlayerPrefs.GetFloat("audiovolume");
                musicVolume = PlayerPrefs.GetFloat("musicvolume");
                graphicTheme = (Themes) PlayerPrefs.GetInt("theme");
            }
            else
            {
                audioVolume = musicVolume = 1f;
                graphicTheme = Themes.Space;
            }
        }

        public static Options LoadSettings()
        {
            return new Options(PlayerPrefs.HasKey("settingsSaved"));
        }
    
        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("volume", audioVolume);
            PlayerPrefs.SetInt("theme", (int) graphicTheme);

            PlayerPrefs.SetString("settingsSaved", "");
        }
    }

    class Hiscore
    {
        public int  tetris, 
                    race, 
                    minigolf, 
                    blockbreaker, 
                    snakeNormal, 
                    snakeSpeed, 
                    total;

        Hiscore(bool load)
        {
            if(load)
            {
                tetris = PlayerPrefs.GetInt("ttScore");
                race = PlayerPrefs.GetInt("rcScore");
                minigolf = PlayerPrefs.GetInt("mgScore");
                blockbreaker = PlayerPrefs.GetInt("bbScore");
                snakeNormal = PlayerPrefs.GetInt("snScore");
                snakeSpeed = PlayerPrefs.GetInt("ssScore");
                total = PlayerPrefs.GetInt("totalScore");
            }
            else
            {
                tetris = race = minigolf = 
                blockbreaker = snakeNormal = 
                snakeSpeed = total = 0;
            }
        }

        public void SaveScore()
        {
            PlayerPrefs.SetInt("ttScore", tetris);
            PlayerPrefs.SetInt("rcScore", race);
            PlayerPrefs.SetInt("mgScore", minigolf);
            PlayerPrefs.SetInt("bbScore", blockbreaker);
            PlayerPrefs.SetInt("snScore", snakeNormal);
            PlayerPrefs.SetInt("ssScore", snakeSpeed);
            PlayerPrefs.SetInt("totalScore", total);
            PlayerPrefs.SetString("scoreSaved", "");
        }

        public static Hiscore LoadScore()
        {
            return new Hiscore(PlayerPrefs.HasKey("scoreSaved"));
        }
    }

    static Hiscore playerScore;
    public static Options playerSettings;
    static bool vaultOpen = false;
    public static Coroutine energyTimer;
    public static int energy;
    public const int ENERGY_MAX = 30;

    //ile punktów trza do odblokowania tt, rc, mg, bb, snek
    public static readonly int[] unlockPoints = {0, 80, 120, 400, 10000, 10000};
    
    //aktulanie włączona gra
    public static GameType currentGame;

    //sprawdzenie czy gra może być odblokowana
    public static bool isUnlocked(GameType game)
    {
        if(!vaultOpen) Open();
        return playerScore.total >= unlockPoints[(int) game];
    }

    //pobiera ilość punktów total
    public static int GetScore()
    {
        if(!vaultOpen) Open();
        return playerScore.total;
    }

    //pobiera rekord z wybranej gry
    public static int GetScore(GameType game)
    {
        if(!vaultOpen) Open();
        switch(game)
        {
            case GameType.Tetris: return playerScore.tetris;
            case GameType.Race: return playerScore.race;
            case GameType.MG: return playerScore.minigolf;
            case GameType.BB: return playerScore.blockbreaker;
            case GameType.SN: return playerScore.snakeNormal;
            case GameType.SS: return playerScore.snakeSpeed;
            default: return 0;
        }
    }

    //dodaje podaną ilość do punktów total
    public static void AddTotalScore(int score)
    {
        if(!vaultOpen) Open();
        playerScore.total += score;
        SaveVault();
    }

    //ustawia rekord podanej gry na podaną wartość
    public static void SetScore(GameType game, int score)
    {
        if(!vaultOpen) Open();
        switch(game)
        {
            case GameType.Tetris: 
                playerScore.tetris = score;
                break;
            case GameType.Race:
                playerScore.race = score;
                break;
            case GameType.MG: 
                playerScore.minigolf = score;
                break;
            case GameType.BB:
                playerScore.blockbreaker = score;
                break;
            case GameType.SN: 
                playerScore.snakeNormal = score;
                break;
            case GameType.SS:
                playerScore.snakeSpeed = score;
                break;
            default: return;
        }

        SaveVault();
    }

    //zapisuje stan gry
    static void SaveVault()
    {
        if(!vaultOpen) Open();
        playerScore.SaveScore();
        playerSettings.SaveSettings();
    }

    //ładuje stan gry
    public static void Open()
    {
        if(vaultOpen) return;

        energy = 20;

        playerScore = Hiscore.LoadScore();
        playerSettings = Options.LoadSettings();

        vaultOpen = true;
    }

    //cheat +100 do totala
    public static void CheatAddPoints()
    {
        playerScore.total += 100;
    }

    //cheat -100 do totala
    public static void CheatSubPoints()
    {
        playerScore.total -= playerScore.total > 100 ? 100 : playerScore.total;
    }
}