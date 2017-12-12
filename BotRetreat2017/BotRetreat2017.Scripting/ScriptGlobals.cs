using System;
using System.Collections.Generic;
using System.Linq;
using BotRetreat2017.Model;
using BotRetreat2017.Scripting.Interfaces;
using BotRetreat2017.Utilities;

namespace BotRetreat2017.Scripting {
  public class ScriptGlobals : IScriptGlobals {

    #region -_ Private Members _-

    //private readonly ILogLogic //_logLogic_a76ef698d9204527836e30719d971888;
    private readonly Bot _e55472035b434b1ea185cf32ece2b8bc;
    private readonly List<Bot> _6f4e9d2c3f474f2fa08d833e05cbba60;

    private readonly Int16 _82f726e404434ffba6e186170deb62c6;
    private readonly Int16 _bb242fc7ea564cdebb9e2e60e1979ff0;
    private Int16 _a269ff57246b40fcb2d16fc97f5dea6c;
    private Int16 _08389405491042dd89325d46355263d1;
    private Int16 _3af8f9c91eb24bd0ab4389ed5241f78c;
    private Int16 _d5a8c937f540473f9d5f40269401f254;
    private Orientation _037c4fa95d8746db916121ade1c5f1d0;
    private readonly LastAction _8c1900d139b84019984c850387e441d6;
    private LastAction _fd832498b5c4470bb4ac626ee3b3952d;
    private Position _lastAttackLocation;
    private Guid? _lastAttackBotId;

    private Int32 _673add7aa39b4601ba69f8713deffc8e;
    private Int32 _b18fb6677a9943b8a074c9bda14d4131;
    private Int32 _9396788906ad4e2b8715c215800c6a98;

    private Dictionary<String, String> _0534fba27330469a120b08d53b05b963;

    #endregion

    #region -_ Constants _-

    public const Int16 MELEE_DAMAGE = 3;
    public const Int16 MELEE_BACKSTAB_DAMAGE = 5;
    public const Int16 RANGED_DAMAGE = 1;
    public const Int16 SELF_DESTRUCT_DAMAGE = 25;
    public const Int16 MAXIMUM_RANGE = 5;

    public const LastAction IDLING = LastAction.Idling;
    public const LastAction TURNING_LEFT = LastAction.TurningLeft;
    public const LastAction TURNING_RIGHT = LastAction.TurningRight;
    public const LastAction TURNING_AROUND = LastAction.TurningAround;
    public const LastAction MOVING_FORWARD = LastAction.MovingForward;
    public const LastAction RANGED_ATTACK = LastAction.RangedAttack;
    public const LastAction MELEE_ATTACK = LastAction.MeleeAttack;
    public const LastAction SELF_DESTRUCTING = LastAction.SelfDestruct;
    public const LastAction SCRIPT_ERROR = LastAction.ScriptError;

    public const Orientation NORTH = Orientation.North;
    public const Orientation EAST = Orientation.East;
    public const Orientation SOUTH = Orientation.South;
    public const Orientation WEST = Orientation.West;

    #endregion

    #region -_ Properties _-

    public Int16 Width => _82f726e404434ffba6e186170deb62c6;
    public Int16 Height => _bb242fc7ea564cdebb9e2e60e1979ff0;
    public Int16 LocationX => _a269ff57246b40fcb2d16fc97f5dea6c;
    public Int16 LocationY => _08389405491042dd89325d46355263d1;
    public Int16 MaximumPhysicalHealth { get; }
    public Int16 PhysicalHealth => _3af8f9c91eb24bd0ab4389ed5241f78c;
    public Int16 PhysicalHealthDrain { get; }
    public Int16 MaximumStamina { get; }
    public Int16 Stamina => _d5a8c937f540473f9d5f40269401f254;
    public Int16 StaminaDrain { get; }
    public Orientation Orientation => _037c4fa95d8746db916121ade1c5f1d0;
    public LastAction LastAction => _8c1900d139b84019984c850387e441d6;
    public LastAction CurrentAction => _fd832498b5c4470bb4ac626ee3b3952d;
    public IFieldOfView Vision { get; }
    public Dictionary<String, String> Memory => _0534fba27330469a120b08d53b05b963;
    public Position LastAttackLocation => _lastAttackLocation;
    public Guid? LastAttackBotId => _lastAttackBotId;
    public Int32 PhysicalDamageDone => _673add7aa39b4601ba69f8713deffc8e;
    public Int32 MentalDamageDone => _b18fb6677a9943b8a074c9bda14d4131;
    public Int32 Kills => _9396788906ad4e2b8715c215800c6a98;

    #endregion

    #region -_ Construction _-

    public ScriptGlobals( Arena arena, Bot bot, List<Bot> bots ) {
      _e55472035b434b1ea185cf32ece2b8bc = bot;
      _6f4e9d2c3f474f2fa08d833e05cbba60 = bots ?? new List<Bot>( );
      _82f726e404434ffba6e186170deb62c6 = arena.Width;
      _bb242fc7ea564cdebb9e2e60e1979ff0 = arena.Height;
      _a269ff57246b40fcb2d16fc97f5dea6c = bot.LocationX;
      _08389405491042dd89325d46355263d1 = bot.LocationY;
      _037c4fa95d8746db916121ade1c5f1d0 = bot.Orientation;
      MaximumPhysicalHealth = bot.MaximumPhysicalHealth;
      _3af8f9c91eb24bd0ab4389ed5241f78c = bot.CurrentPhysicalHealth;
      PhysicalHealthDrain = bot.PhysicalHealthDrain;
      MaximumStamina = bot.MaximumStamina;
      _d5a8c937f540473f9d5f40269401f254 = bot.CurrentStamina;
      StaminaDrain = bot.StaminaDrain;
      _8c1900d139b84019984c850387e441d6 = bot.LastAction;
      _fd832498b5c4470bb4ac626ee3b3952d = LastAction.Idling;
      _lastAttackLocation = new Position { X = -1, Y = -1 };
      Vision = new FieldOfView( arena, bot, bots );
      _0534fba27330469a120b08d53b05b963 = bot.Memory.Deserialize<Dictionary<String, String>>( ) ?? new Dictionary<String, String>( );
      ////_logLogic_a76ef698d9204527836e30719d971888 = logLogic;
    }

    #endregion

    #region -_ Public Methods _-

    public void MoveForward( ) {
      if( CurrentAction == LastAction.Idling && Stamina > 0 && !WillColide( ) ) {
        _d5a8c937f540473f9d5f40269401f254--;
        switch( Orientation ) {
          case Orientation.North:
            _08389405491042dd89325d46355263d1--;
            _fd832498b5c4470bb4ac626ee3b3952d = LastAction.MovingForward;
            //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotMovingNorth);
            break;
          case Orientation.East:
            _a269ff57246b40fcb2d16fc97f5dea6c++;
            _fd832498b5c4470bb4ac626ee3b3952d = LastAction.MovingForward;
            //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotMovingEast);
            break;
          case Orientation.South:
            _08389405491042dd89325d46355263d1++;
            _fd832498b5c4470bb4ac626ee3b3952d = LastAction.MovingForward;
            //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotMovingSouth);
            break;
          case Orientation.West:
            _a269ff57246b40fcb2d16fc97f5dea6c--;
            _fd832498b5c4470bb4ac626ee3b3952d = LastAction.MovingForward;
            //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotMovingWest);
            break;
        }
      }
    }

    public void TurnLeft( ) {
      if( CurrentAction == LastAction.Idling ) {
        Turn( -1 );
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.TurningLeft;
        //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotTurningLeft);
      }
    }

    public void TurnRight( ) {
      if( CurrentAction == LastAction.Idling ) {
        Turn( +1 );
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.TurningRight;
        //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotTurningRight);
      }
    }

    public void TurnAround( ) {
      if( CurrentAction == LastAction.Idling ) {
        Turn( +2 );
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.TurningAround;
        //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotTurningAround);
      }
    }

    public void Teleport( Int16 x, Int16 y ) {
      if( CurrentAction == LastAction.Idling && Stamina > 0 && !WillColide( x, y ) ) {
        _d5a8c937f540473f9d5f40269401f254 -= 10;

        _a269ff57246b40fcb2d16fc97f5dea6c = x;
        _08389405491042dd89325d46355263d1 = y;
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.Teleport;
      }
    }

    public void MeleeAttack( ) {
      if( CurrentAction == LastAction.Idling ) {
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.MeleeAttack;
        var botInMelee = FindBotInRange( );
        if( botInMelee != null ) {
          var bot = FindBot( botInMelee );
          _lastAttackBotId = bot.Id;
          _lastAttackLocation = new Position { X = bot.LocationX, Y = bot.LocationY };
          if( botInMelee.Orientation == Orientation ) {
            DamageBot( bot, MELEE_BACKSTAB_DAMAGE );
            _673add7aa39b4601ba69f8713deffc8e += MELEE_BACKSTAB_DAMAGE;
            //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotMeleeBackstabAttack);
          } else {
            DamageBot( bot, MELEE_DAMAGE );
            _673add7aa39b4601ba69f8713deffc8e += MELEE_DAMAGE;
            //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotMeleeAttack);
          }
        }
      }
    }

    public void RangedAttack( Int16 x, Int16 y ) {
      if( CurrentAction == LastAction.Idling ) {
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.RangedAttack;
        var rangeDistance = CalculateRangeDistance( x, y );
        _lastAttackLocation = rangeDistance > MAXIMUM_RANGE ? CalculateRangeImpact( x, y ) : new Position { X = x, Y = y };
        var botInRange = FindBotInRange( _lastAttackLocation.X, _lastAttackLocation.Y );
        if( botInRange != null && rangeDistance <= MAXIMUM_RANGE ) {
          var bot = FindBot( botInRange );
          _lastAttackBotId = bot.Id;
          DamageBot( bot, RANGED_DAMAGE );
          _673add7aa39b4601ba69f8713deffc8e += RANGED_DAMAGE;
          //_logLogic_a76ef698d9204527836e30719d971888.LogMessage(null, null, _bot_e55472035b434b1ea185cf32ece2b8bc, HistoryName.BotRangedAttack);
        }
      }
    }

    public void SelfDestruct( ) {
      if( CurrentAction == LastAction.Idling ) {
        _fd832498b5c4470bb4ac626ee3b3952d = LastAction.SelfDestruct;
        var botsInMelee = FindBotsInRange( );
        foreach( var botInMelee in botsInMelee ) {
          DamageBot( botInMelee, SELF_DESTRUCT_DAMAGE );
          _673add7aa39b4601ba69f8713deffc8e += SELF_DESTRUCT_DAMAGE;
        }
        _3af8f9c91eb24bd0ab4389ed5241f78c = 0;
      }
    }

    public void StoreInMemory<T>( String key, T value ) {
      if( _0534fba27330469a120b08d53b05b963.ContainsKey( key ) ) {
        _0534fba27330469a120b08d53b05b963[key] = value.Serialize( );
      } else {
        _0534fba27330469a120b08d53b05b963.Add( key, value.Serialize( ) );
      }
    }

    public T LoadFromMemory<T>( String key ) {
      if( _0534fba27330469a120b08d53b05b963.ContainsKey( key ) ) {
        return _0534fba27330469a120b08d53b05b963[key].Deserialize<T>( );
      }
      return default( T );
    }

    public void RemoveFromMemory( String key ) {
      if( _0534fba27330469a120b08d53b05b963.ContainsKey( key ) ) {
        _0534fba27330469a120b08d53b05b963.Remove( key );
      }
    }

    #endregion

    #region -_ Private Helper Methods _-

    private Bot FindBot( IBot bot ) {
      return _6f4e9d2c3f474f2fa08d833e05cbba60.Single( x => x.Name == bot.Name );
    }

    private IBot FindBotInRange( ) {
      IBot botInRange = null;
      switch( Orientation ) {
        case Orientation.North:
          botInRange = Vision.Bots
              .SingleOrDefault( b => b.Location.X == _a269ff57246b40fcb2d16fc97f5dea6c && b.Location.Y == _08389405491042dd89325d46355263d1 - 1 );
          break;
        case Orientation.East:
          botInRange = Vision.Bots
              .SingleOrDefault( b => b.Location.X == _a269ff57246b40fcb2d16fc97f5dea6c + 1 && b.Location.Y == _08389405491042dd89325d46355263d1 );
          break;
        case Orientation.South:
          botInRange = Vision.Bots
              .SingleOrDefault( b => b.Location.X == _a269ff57246b40fcb2d16fc97f5dea6c && b.Location.Y == _08389405491042dd89325d46355263d1 + 1 );
          break;
        case Orientation.West:
          botInRange = Vision.Bots
              .SingleOrDefault( b => b.Location.X == _a269ff57246b40fcb2d16fc97f5dea6c - 1 && b.Location.Y == _08389405491042dd89325d46355263d1 );
          break;
      }
      return botInRange;
    }

    private IEnumerable<Bot> FindBotsInRange( ) {
      var botsInRange = new List<Bot>( );
      var northBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c && x.LocationY == _08389405491042dd89325d46355263d1 - 1 );
      if( northBot != null ) {
        botsInRange.Add( northBot );
      }
      var northEastBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c + 1 && x.LocationY == _08389405491042dd89325d46355263d1 - 1 );
      if( northEastBot != null ) {
        botsInRange.Add( northEastBot );
      }
      var eastBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c + 1 && x.LocationY == _08389405491042dd89325d46355263d1 );
      if( eastBot != null ) {
        botsInRange.Add( eastBot );
      }
      var southEastBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c + 1 && x.LocationY == _08389405491042dd89325d46355263d1 + 1 );
      if( southEastBot != null ) {
        botsInRange.Add( southEastBot );
      }
      var southBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c && x.LocationY == _08389405491042dd89325d46355263d1 + 1 );
      if( southBot != null ) {
        botsInRange.Add( southBot );
      }
      var southWestBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c - 1 && x.LocationY == _08389405491042dd89325d46355263d1 + 1 );
      if( southWestBot != null ) {
        botsInRange.Add( southWestBot );
      }
      var westBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c - 1 && x.LocationY == _08389405491042dd89325d46355263d1 );
      if( westBot != null ) {
        botsInRange.Add( westBot );
      }
      var northWestBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( x => x.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c - 1 && x.LocationY == _08389405491042dd89325d46355263d1 - 1 );
      if( northWestBot != null ) {
        botsInRange.Add( northWestBot );
      }
      return botsInRange;
    }

    private IBot FindBotInRange( Int16 x, Int16 y ) {
      return Vision.Bots.SingleOrDefault( b => b.Location.X == x && b.Location.Y == y );
    }

    private Decimal CalculateRangeDistance( Int16 x, Int16 y ) {
      var distanceOnX = Math.Abs( _a269ff57246b40fcb2d16fc97f5dea6c - x );
      var distanceOnY = Math.Abs( _08389405491042dd89325d46355263d1 - y );
      return (Decimal) Math.Sqrt( distanceOnX * distanceOnX + distanceOnY * distanceOnY );
    }

    private Position CalculateRangeImpact( Int16 x, Int16 y ) {
      return new Position { X = x, Y = y };
    }

    private Boolean WillColide( ) {
      Boolean colidingEdge = false;
      Bot colidingBot = null;
      switch( Orientation ) {
        case Orientation.North:
          if( _08389405491042dd89325d46355263d1 == 0 ) colidingEdge = true;
          colidingBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( b => b.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c && b.LocationY == _08389405491042dd89325d46355263d1 - 1 );
          break;
        case Orientation.East:
          if( _a269ff57246b40fcb2d16fc97f5dea6c == Width - 1 ) colidingEdge = true;
          colidingBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( b => b.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c + 1 && b.LocationY == _08389405491042dd89325d46355263d1 );
          break;
        case Orientation.South:
          if( _08389405491042dd89325d46355263d1 == Height - 1 ) colidingEdge = true;
          colidingBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( b => b.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c && b.LocationY == _08389405491042dd89325d46355263d1 + 1 );
          break;
        case Orientation.West:
          if( _a269ff57246b40fcb2d16fc97f5dea6c == 0 ) colidingEdge = true;
          colidingBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( b => b.LocationX == _a269ff57246b40fcb2d16fc97f5dea6c - 1 && b.LocationY == _08389405491042dd89325d46355263d1 );
          break;
      }
      return colidingBot != null || colidingEdge;
    }

    private Boolean WillColide( Int16 x, Int16 y ) {
      Boolean outsideArena = x < 0 || x > Width - 1 || y < 0 || y > Height - 1;
      Bot colidingBot = _6f4e9d2c3f474f2fa08d833e05cbba60.SingleOrDefault( b => b.LocationX == x && b.LocationY == y );

      return colidingBot != null || !outsideArena;
    }

    private void Turn( SByte turn ) {
      _037c4fa95d8746db916121ade1c5f1d0 = (Orientation) ( ( ( (Byte) Orientation + turn ) + 4 ) % 4 );
    }

    private void DamageBot( Bot bot, Int16 damage ) {
      if( bot.CurrentPhysicalHealth > 0 ) {
        var newPhysicalHealth = (Int16) ( bot.CurrentPhysicalHealth - damage );
        bot.CurrentPhysicalHealth = newPhysicalHealth < 0 ? (Int16) 0 : newPhysicalHealth;
        if( bot.CurrentPhysicalHealth == 0 ) {
          bot.LastAction = LastAction.Died;
          bot.TimeOfDeath = DateTime.UtcNow;
          _9396788906ad4e2b8715c215800c6a98++;
        }
      }
    }

    #endregion

  }
}