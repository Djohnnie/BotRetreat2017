using BotRetreat2017.Model;
using BotRetreat2017.Utilities;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace BotRetreat2017.Scripting.Tests {
  [TestClass]
  public class MoveForwardTests {
    [TestMethod]
    public async Task Bot_Cannot_MoveForward_With_North_Orientation_On_1x1_Arena( ) {
      // Arrange
      Arena arena = new Arena { Width = 1, Height = 1 };
      Team team = new Team { Name = "Team!" };
      Bot bot = new Bot { LocationX = 0, LocationY = 0, Orientation = Orientation.North, MaximumStamina = 1, CurrentStamina = 1, StaminaDrain = 0 };
      Deployment deployment = new Deployment { Arena = arena, Team = team, Bot = bot };
      bot.Deployments = ( new[] { deployment } ).ToList( );

      Script botScript = await BotScript.PrepareScript( "MoveForward();".Base64Encode( ) );
      botScript.Compile( );
      ScriptGlobals globals = new ScriptGlobals( arena, bot, new List<Bot>( ) );

      // Act
      await botScript.RunAsync( globals );

      // Assert
      Assert.AreEqual( 0, globals.LocationX );
      Assert.AreEqual( 0, globals.LocationY );
      Assert.AreEqual( 1, globals.Stamina );
      Assert.AreEqual( 0, globals.StaminaDrain );
    }
  }
}