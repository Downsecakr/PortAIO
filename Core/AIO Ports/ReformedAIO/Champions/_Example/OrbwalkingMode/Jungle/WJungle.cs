using EloBuddy; 
using LeagueSharp.Common; 
namespace ReformedAIO.Champions._Example.OrbwalkingMode.Jungle
{
    using System;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ReformedAIO.Champions._Example.Core.Spells;

    using RethoughtLib.FeatureSystem.Implementations;

    internal sealed class WJungle : OrbwalkingChild
    {
        public override string Name { get; set; } = "W";

        private readonly WSpell spell;

        public WJungle(WSpell spell)
        {
            this.spell = spell;
        }

        private Obj_AI_Base Mob =>
              MinionManager.GetMinions(ObjectManager.Player.Position,
                  spell.Spell.Range,
                  MinionTypes.All,
                  MinionTeam.Neutral).FirstOrDefault();

        private void OnUpdate(EventArgs args)
        {
            if (!CheckGuardians()
                || Mob == null
                || Menu.Item("Example.Jungle.W.Mana").GetValue<Slider>().Value > ObjectManager.Player.ManaPercent)
            {
                return;
            }


        }

        protected override void OnDisable(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnDisable(sender, eventArgs);

            Game.OnUpdate -= OnUpdate;
        }

        protected override void OnEnable(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnEnable(sender, eventArgs);

            Game.OnUpdate += OnUpdate;
        }

        protected override void OnLoad(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnLoad(sender, eventArgs);


            Menu.AddItem(new MenuItem("Example.Jungle.W.Mana", "Min Mana %").SetValue(new Slider(0, 0, 100)));
        }
    }
}
