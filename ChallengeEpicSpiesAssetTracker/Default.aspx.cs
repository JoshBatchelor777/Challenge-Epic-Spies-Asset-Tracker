using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeEpicSpiesAssetTracker
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // create an array the page will remember so more values can be added
                // do ^ this ^ for each text box. 
                string[] name = new string[0];
                ViewState.Add("Name", name);

                double[] acts = new double[0];
                ViewState.Add("Acts", acts);

                double[] elections = new double[0];
                ViewState.Add("Elections", elections);


                /* 
                 * This Code was not used, but I like the concept anyways.
                 * 2d arrays apparently cannot be stored into the ViewState.
                 * So instead, I just serialized the arrays into 3 
                 * one-dimencional arrays. 
                // Using a Jagged Array, create a single dimensional
                // array with 3 elements, each of which is another
                // single dimensional array.

                int[][] memory = new int[3][];
                memory[0] = new int[] { name[5] };
                memory[1] = new int[] { acts[5] };
                memory[2] = new int[] { elections[5] };
                // Then just store all of that into a single array?
                ViewState.Add("Memory", memory);
                */
                
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            // Invoke the values stored in said ^ array when the button is clicked
            string[] name = (string[])ViewState["Name"];
            double[] acts = (double[])ViewState["Acts"];
            double[] elections = (double[])ViewState["Elections"];

            // Create new array to store the new values being entered per 
            // page reload / button click.
            Array.Resize(ref name, name.Length + 1);
            Array.Resize(ref acts, acts.Length + 1);
            Array.Resize(ref elections, elections.Length + 1);
            // Grab the most recent element from the new array
            // and store it in 'newestItem' etc.
            int newestItemName = name.GetUpperBound(0);
            int newestItemActs = acts.GetUpperBound(0);
            int newestItemElections = elections.GetUpperBound(0);

            // Now convert the most recent element into something 
            // I can print to the resultLabel.
            name[newestItemName] = assetTextBox.Text;
            acts[newestItemActs] = double.Parse(subterfugeTextBox.Text);
            elections[newestItemElections] = double.Parse(electionsTextBox.Text);
            // then put it back into the page memory array...right?
            ViewState["Name"] = name; // Replace the old array with the new
            ViewState["Acts"] = acts;
            ViewState["Elections"] = elections;

            // This bit may be redundant. However, I want to be sure 
            // this element is being converted to a printable string.
            string newName = name[newestItemName].ToString();


            // Finally, print out the name string, the average acts per asset,
            // and lastly, the number of elections rigged.
            resultLabel.Text = String.Format("Total elections rigged: {0} <br /> " +
                "Average Acts of subterfuge per asset: {1:N2} <br /> " + 
                "(Last Asset you Added: {2})",
                elections.Sum(),
                acts.Average(),
                newName);
        }
    }
}