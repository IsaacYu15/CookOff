using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public Transform playerOneSpawnPosition;
    public Transform playerTwoSpawnPosition;
    public Transform playerOnePosition;
    public Transform playerTwoPosition;

    public TextMeshProUGUI timer; 
    public float maxTime = 100;
    public float currTime;

    public TextMeshProUGUI itemToCook;
    public int numberItems = 3;
    public int cubedChickens;
    public int cubedSteaks;
    public int cubedLettuces;
    public int tacos;

    public GameObject settingDisplay;
    public GameObject duringCookDisplay;
    public GameObject resultsDisplay;

    public GameObject playerOneHeader;
    public GameObject playerTwoHeader;
    public GetDishItems playerOneTableSubmission;
    public GetDishItems playerTwoTableSubmission;
    public Color green;
    public Color red;

    public TextMeshProUGUI winMessage;
    private string playerOneWin = "WINNER: PLAYER ONE";    
    private string playerTwoWin = "WINNER: PLAYER TWO";

    public TMP_InputField itemsToCookInputField;
    public TMP_InputField timerInputField;


    private bool intializedGame;

    public void Start()
    {
        //set up UI
        settingDisplay.SetActive(true);
        duringCookDisplay.SetActive(false);
        resultsDisplay.SetActive(false);

        //make sure players cannot move during settings
        playerOnePosition.gameObject.GetComponent<PlayerMovement>().enabled = false;
        playerTwoPosition.gameObject.GetComponent<PlayerMovement>().enabled = false;

    }
    private void Update()
    {
        if (intializedGame)
        {
            if (currTime > 0) //during gameplay
            {
                duringCookDisplay.SetActive(true);
                resultsDisplay.SetActive(false);
                updateTimer();
            }
            else //results
            {
                duringCookDisplay.SetActive(false);
                resultsDisplay.SetActive(true);
                winMessage.text = getWinner();
            }

        }
    }

    private void updateTimer ()
    {
        //update timer to show seconds and minutes
        currTime -= Time.deltaTime;

        int minutes = (int)currTime / 60;
        int seconds = (int)currTime - minutes * 60;

        if (seconds < 10)
        {
            timer.text = minutes.ToString() + " : 0" + seconds.ToString();
        }
        else
        {
            timer.text = minutes.ToString() + " : " + seconds.ToString();
        }
    }

    private void generateRandomDish (int items)
    {
        //default values
        cubedChickens = 0;
        cubedSteaks = 0;
        cubedLettuces = 0;
        tacos = 1;
        
        for (int i = 0; i < items; i ++)// for number of items...
        {
            //random number generation to determine items
            int random = Random.Range(0, 3);

            if (random == 0)
            {
                cubedChickens++; //increement
            } else if (random == 1)
            {
                cubedSteaks++;
            } else if (random == 2)
            {
                cubedLettuces++;
            } 
        }

        //string concataion to create the final requested item
        string request = "ITEM:   ";

        if (cubedChickens > 0)
        {
            request += cubedChickens + "  chickens  ,  ";
        }
        if (cubedSteaks > 0)
        {
            request += cubedSteaks + "  steaks  ,  ";
        }
        if (cubedLettuces > 0)
        {
            request += cubedLettuces + "  lettuce  ,  ";
        }

        request += tacos + "  taco";

        itemToCook.text = request;

        //reset currTime
        currTime = maxTime;
    }

    public string getWinner ()
    {
        playerOneTableSubmission.getDish();
        playerTwoTableSubmission.getDish();

        float playerOneScore = getScore(playerOneTableSubmission, playerOneHeader);
        float playerTwoScore = getScore(playerTwoTableSubmission, playerTwoHeader);

        //if the players have the same score, compare their time
        if (playerOneScore == playerTwoScore)
        {
            if (playerOneTableSubmission.timer == playerTwoTableSubmission.timer)
            {
                return "Tie game!";
            } else if (playerOneTableSubmission.timer < playerTwoTableSubmission.timer)
            {
                return playerTwoWin;
            } else
            {
                return playerOneWin;
            }
        }
        else if (playerOneScore > playerTwoScore) //player with highest score wins
        {
            return playerOneWin;
        }
        else
        {
            return playerTwoWin;
        }
    }

    public float getScore (GetDishItems dish, GameObject header)
    {
        //for labeling the UI
        int mistakes = 0;

        header.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Tacos: " + dish.tacos;
        header.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Steaks: " + dish.cubedSteaks;
        header.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Chicken: " + dish.cubedChickens;
        header.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Lettuce: " + dish.cubedLettuce;
        header.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Wrong items: " + dish.wrongItems;

        header.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = green;
        header.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = green;
        header.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = green;
        header.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = green;
        header.transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = green;

        //count up mistakes as the difference between requested and actually placed items (Abs is used as could be more or less items than requested)
        if (tacos != dish.tacos) { 
            mistakes += Mathf.Abs (tacos - dish.tacos);
            header.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = red;
        }
        if (cubedSteaks != dish.cubedSteaks) { 
            mistakes+= Mathf.Abs (cubedSteaks - dish.cubedSteaks);
            header.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = red;
        }
        if (cubedChickens != dish.cubedChickens) {
            mistakes += Mathf.Abs(cubedChickens - dish.cubedChickens);
            header.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = red;
        }
        if (cubedLettuces != dish.cubedLettuce) {
            mistakes += Mathf.Abs(cubedLettuces - dish.cubedLettuce);
            header.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = red;
        }

        if (dish.wrongItems > 0)
        {
            mistakes += dish.wrongItems;
            header.transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = red;
        }
        
        if (dish.timer < 0)
        {
            dish.timer = 0;
        }
        //round all decimal value
        header.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Submission Time: " + Mathf.Round(dish.timer * 100f) / 100f + "(s)";

        //scoring will take an emphasis on the actually dish (weighted 70%) and submission time (weighted 30%)
        int dishScore = Mathf.Max(1000 - mistakes * 100, 0);
        float timeScore = Mathf.Max (dishScore * (dish.timer / maxTime), dishScore * 0.3f); //using max ensures time score is not too low

        float score = dishScore * 0.7f + timeScore * 0.3f;

        header.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Score: " + Mathf.Round(score * 100f) / 100f + "pts";


        return score;

    }

    public void intializeGame()
    {
        //get all values from the text field
        if (int.TryParse(itemsToCookInputField.text, out numberItems))
        {
            numberItems = int.Parse(itemsToCookInputField.text);
        }
        else
        {
            //if the user decides to input something invalid
            numberItems = 3;
            Debug.Log("Invalid Input - number of items to cook defaulted to: " + numberItems);
        }

        if (float.TryParse(timerInputField.text, out maxTime))
        {
            maxTime = float.Parse(timerInputField.text);
        }
        else
        {
            maxTime = 100;
            Debug.Log("Invalid Input - max time defaulted to: " + maxTime);
        }

        resetGame();

        //move players to spawn locations
        intializedGame = true;
        playerOnePosition.gameObject.GetComponent<PlayerMovement>().enabled = true;
        playerTwoPosition.gameObject.GetComponent<PlayerMovement>().enabled = true;

        settingDisplay.SetActive(false);
    }

    public void resetGame ()
    {
        generateRandomDish(numberItems);

        //open and close repsective UI
        duringCookDisplay.SetActive(true);
        resultsDisplay.SetActive(false);

        playerOneTableSubmission.resetTable();
        playerTwoTableSubmission.resetTable();

        //move players to spawn locaiton
        playerOnePosition.position = playerOneSpawnPosition.position;
        playerTwoPosition.position = playerTwoSpawnPosition.position;

        currTime = maxTime;

        //destroy all items 
        GameObject [] allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        for (int i = 0; i < allGameObjects.Length; i ++)
        {
            if (allGameObjects[i] != null)
            {
                if (allGameObjects[i].layer == LayerMask.NameToLayer("item"))
                {
                    Destroy(allGameObjects[i]);
                }
            }

        }

    }


}
