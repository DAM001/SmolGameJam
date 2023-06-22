using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static List<GameObject> Items = new List<GameObject>();
    public static List<GameObject> Characters = new List<GameObject>();

    public void SetupData()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            Items.Add(items[i]);
        }

        Characters.Add(GameObject.FindGameObjectWithTag("Player"));
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < players.Length; i++)
        {
            Characters.Add(players[i]);
        }
    }

    public static string[] Names = new string[100]
    {
        "Sneeze Slinger",
        "Bacteria Buster",
        "Microbe Mischief",
        "Germzilla",
        "Flu-Flinging Fiend",
        "Snotty Bandit",
        "The Infection Injection",
        "The Itty-Bitty Bug",
        "Coughing Crawler",
        "The Slimy Squirm",
        "The Sneaky Sniffle",
        "Plague Pipsqueak",
        "Virus Vandal",
        "The Tiniest Troublemaker",
        "Infectious Imp",
        "Gooey Grappler",
        "The Microscopic Menace",
        "The Runny Nose Rascal",
        "Pus Patrol",
        "The Germ Guru",
        "The Bacterial Buccaneer",
        "Sneezing Serpent",
        "The Moldy Marauder",
        "The Snotling",
        "Contagion Commander",
        "Germ Gangster",
        "The Spore Sniper",
        "The Rotting Rogue",
        "The Sticky Slimer",
        "The Germaphobe's Nightmare",
        "Laughing Legionella",
        "Wobbly Wormwood",
        "Rascally Rhinovirus",
        "Prancing Pneumococcus",
        "Bubbly Bordetella",
        "Grinning Gonorrhea",
        "Wailing Staphylococcus",
        "Dizzy Diphtheria",
        "Quivering Quinolone",
        "Tickle Toxoplasma",
        "Grumpy Gastroenteritis",
        "Chuckling Cholera",
        "Mumbling Measles",
        "Jolly Jiggers",
        "Dapper Dysentery",
        "Snickering Streptococcus",
        "Sassy Syphilis",
        "Whirling Whipple's",
        "Merry Malaria",
        "Hopping Helicobacter",
        "Winking Worms",
        "Sneezing Salmonella",
        "Bouncy Blastocystis",
        "Giggling Giardia",
        "Silly Shigella",
        "Chuckleful Cryptosporidium",
        "Snickering Norovirus",
        "Hilarious Haemophilus",
        "Giddy Gardnerella",
        "Chortling Chikungunya",
        "Wriggling Whooping Cough",
        "Ticklish Trichomonas",
        "Wobblebottom Warts",
        "Dancing Dermatophyte",
        "Hysterical H1N1",
        "Snickering Sarcoptes",
        "Quirky Q Fever",
        "Rambunctious Rotavirus",
        "Wheezing West Nile",
        "Fluttering Flesh-Eating",
        "Fizzy Fungus",
        "Jiggly Jock Itch",
        "Giddy Gingivitis",
        "Hopping Herpes",
        "Squirmy Scabies",
        "Wacky Watery Eyes",
        "Bouncing Botulism",
        "Giggly Gangrene",
        "Quirky Queso Fever",
        "Dizzying Dengue",
        "Silly Syringomyelia",
        "Chuckling Candida",
        "Snickering Swine Flu",
        "Mirthful Meningitis",
        "Grinning Giardiasis",
        "Whimsical Whoopie Cushion",
        "Snickering Scrofula",
        "Quivering Quinsy",
        "Blushing Blastomycosis",
        "Jolly Jungle Rot",
        "Frisky Fungemia",
        "Teasing Thrush",
        "Hopping Histoplasmosis",
        "Whistling Westie Warts",
        "Smirking Strep Throat",
        "Bouncy Black Mold",
        "Rumbling Rocky Mountain Spotted Fever",
        "Chatty Chigger",
        "Witty Weil's Disease",
        "Merry MRSA"
    };
}
