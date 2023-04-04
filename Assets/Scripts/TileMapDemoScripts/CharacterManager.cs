using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterManager
{
    private List<Player> characters;
    private const float SPEED = 10.0f;
    private PathFinding pathFinding;

    public CharacterManager(Tilemap tileMap)
    {
        characters = new List<Player>();
        pathFinding = new PathFinding(tileMap);
    }

    public void AddCharacter(Player character)
    {
        characters.Add(character);
    }

    // Set characters target location and target level
    public void setCharactersPath(Vector3Int tileLocation, int level)
    {
        foreach (Player character in characters) {
            character.setTargetLevel(level);
            character.setTargetLocation(tileLocation);
	    }
    }

    public void UpdateCharacters(List<Level> mapLevels)
    {
        foreach(Player character in characters) {
            if (character.getPathCount() == 0 && (character.transform.position != character.getTargetLocation()))
            {
                // Move character to next level
                if (character.transform.position == mapLevels[character.getCurrentLevel()].getStairsPosition() &&
		            character.getCurrentLevel() != character.getTargetLevel())
                {
                    int levelChange;
                    if (character.getCurrentLevel() < character.getTargetLevel()) levelChange = 1;
                    else levelChange = -1;

                    character.transform.position = mapLevels[character.getCurrentLevel() + levelChange].getStairsPosition();
                    character.setCurrentLevel(character.getCurrentLevel() + levelChange);
                }

                // If character's target location is on a different level, set path to stairs
                if (character.getCurrentLevel() != character.getTargetLevel())
                {
                    Vector3Int characterPosition = Vector3Int.FloorToInt(character.transform.position);
                    Vector3Int stairsPosition = mapLevels[character.getCurrentLevel()].getStairsPosition();
                    Level level = mapLevels[character.getCurrentLevel()];
                    character.updatePath(pathFinding.getPath(characterPosition, stairsPosition, level));
                }
                // Target location is on the character's level, set path to target location.
                else
                {
                    Vector3Int characterPosition = Vector3Int.FloorToInt(character.transform.position);
                    Vector3Int target = character.getTargetLocation();
                    Level level = mapLevels[character.getCurrentLevel()];
                    character.updatePath(pathFinding.getPath(characterPosition, target, level));
                }
            }
            character.updateLocation(SPEED * Time.deltaTime);
	    }
    }
}
