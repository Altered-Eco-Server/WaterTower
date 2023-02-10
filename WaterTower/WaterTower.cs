namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Gameplay.Housing.PropertyValues;

    [Serialized]
    [RequireComponent(typeof(LiquidProducerComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]

    public partial class WaterTowerObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Water Tower"); } }
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        public virtual Type RepresentedItemType { get { return typeof(WaterTowerItem); } }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<PowerConsumptionComponent>().Initialize(200);
            this.GetComponent<PowerGridComponent>().Initialize(20, new MechanicalPower());
            this.GetComponent<LiquidProducerComponent>().Setup(typeof(WaterItem), 1.8f, this.GetOccupancyType(BlockOccupancyType.WaterOut));
            this.ModsPostInitialize();
        }

        static WaterTowerObject()
        {
            AddOccupancy<WaterTowerObject>(new List<BlockOccupancy>()
            {
            //Base
			new BlockOccupancy(new Vector3i(0, 0, -3), typeof(PipeSlotBlock), new Quaternion(0f, -1f, 0f, 0f), BlockOccupancyType.WaterOut),
            new BlockOccupancy(new Vector3i(0, 0, -2)),
            new BlockOccupancy(new Vector3i(0, 0, -1)),
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 0, 2)),
            new BlockOccupancy(new Vector3i(1, 0, -2)),
            new BlockOccupancy(new Vector3i(1, 0, -1)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 2)),
            new BlockOccupancy(new Vector3i(2, 0, -2)),
            new BlockOccupancy(new Vector3i(2, 0, -1)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 2)),
            new BlockOccupancy(new Vector3i(-1, 0, -2)),
            new BlockOccupancy(new Vector3i(-1, 0, -1)),
            new BlockOccupancy(new Vector3i(-1, 0, 0)),
            new BlockOccupancy(new Vector3i(-1, 0, 1)),
            new BlockOccupancy(new Vector3i(-1, 0, 2)),
            new BlockOccupancy(new Vector3i(-2, 0, -2)),
            new BlockOccupancy(new Vector3i(-2, 0, -1)),
            new BlockOccupancy(new Vector3i(-2, 0, 0)),
            new BlockOccupancy(new Vector3i(-2, 0, 1)),
            new BlockOccupancy(new Vector3i(-2, 0, 2)),
			//Second Row
			new BlockOccupancy(new Vector3i(0, 1, -2)),
            new BlockOccupancy(new Vector3i(0, 1, -1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 2)),
            new BlockOccupancy(new Vector3i(1, 1, -2)),
            new BlockOccupancy(new Vector3i(1, 1, -1)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 2)),
            new BlockOccupancy(new Vector3i(2, 1, -2)),
            new BlockOccupancy(new Vector3i(2, 1, -1)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 2)),
            new BlockOccupancy(new Vector3i(-1, 1, -2)),
            new BlockOccupancy(new Vector3i(-1, 1, -1)),
            new BlockOccupancy(new Vector3i(-1, 1, 0)),
            new BlockOccupancy(new Vector3i(-1, 1, 1)),
            new BlockOccupancy(new Vector3i(-1, 1, 2)),
            new BlockOccupancy(new Vector3i(-2, 1, -2)),
            new BlockOccupancy(new Vector3i(-2, 1, -1)),
            new BlockOccupancy(new Vector3i(-2, 1, 0)),
            new BlockOccupancy(new Vector3i(-2, 1, 1)),
            new BlockOccupancy(new Vector3i(-2, 1, 2)),
			//Third Row
			new BlockOccupancy(new Vector3i(0, 2, -2)),
            new BlockOccupancy(new Vector3i(0, 2, -1)),
            new BlockOccupancy(new Vector3i(0, 2, 0)),
            new BlockOccupancy(new Vector3i(0, 2, 1)),
            new BlockOccupancy(new Vector3i(0, 2, 2)),
            new BlockOccupancy(new Vector3i(1, 2, -2)),
            new BlockOccupancy(new Vector3i(1, 2, -1)),
            new BlockOccupancy(new Vector3i(1, 2, 0)),
            new BlockOccupancy(new Vector3i(1, 2, 1)),
            new BlockOccupancy(new Vector3i(1, 2, 2)),
            new BlockOccupancy(new Vector3i(2, 2, -2)),
            new BlockOccupancy(new Vector3i(2, 2, -1)),
            new BlockOccupancy(new Vector3i(2, 2, 0)),
            new BlockOccupancy(new Vector3i(2, 2, 1)),
            new BlockOccupancy(new Vector3i(2, 2, 2)),
            new BlockOccupancy(new Vector3i(-1, 2, -2)),
            new BlockOccupancy(new Vector3i(-1, 2, -1)),
            new BlockOccupancy(new Vector3i(-1, 2, 0)),
            new BlockOccupancy(new Vector3i(-1, 2, 1)),
            new BlockOccupancy(new Vector3i(-1, 2, 2)),
            new BlockOccupancy(new Vector3i(-2, 2, -2)),
            new BlockOccupancy(new Vector3i(-2, 2, -1)),
            new BlockOccupancy(new Vector3i(-2, 2, 0)),
            new BlockOccupancy(new Vector3i(-2, 2, 1)),
            new BlockOccupancy(new Vector3i(-2, 2, 2)),
			//Fourth Row
			new BlockOccupancy(new Vector3i(0, 3, -2)),
            new BlockOccupancy(new Vector3i(0, 3, -1)),
            new BlockOccupancy(new Vector3i(0, 3, 0)),
            new BlockOccupancy(new Vector3i(0, 3, 1)),
            new BlockOccupancy(new Vector3i(0, 3, 2)),
            new BlockOccupancy(new Vector3i(1, 3, -2)),
            new BlockOccupancy(new Vector3i(1, 3, -1)),
            new BlockOccupancy(new Vector3i(1, 3, 0)),
            new BlockOccupancy(new Vector3i(1, 3, 1)),
            new BlockOccupancy(new Vector3i(1, 3, 2)),
            new BlockOccupancy(new Vector3i(2, 3, -2)),
            new BlockOccupancy(new Vector3i(2, 3, -1)),
            new BlockOccupancy(new Vector3i(2, 3, 0)),
            new BlockOccupancy(new Vector3i(2, 3, 1)),
            new BlockOccupancy(new Vector3i(2, 3, 2)),
            new BlockOccupancy(new Vector3i(-1, 3, -2)),
            new BlockOccupancy(new Vector3i(-1, 3, -1)),
            new BlockOccupancy(new Vector3i(-1, 3, 0)),
            new BlockOccupancy(new Vector3i(-1, 3, 1)),
            new BlockOccupancy(new Vector3i(-1, 3, 2)),
            new BlockOccupancy(new Vector3i(-2, 3, -2)),
            new BlockOccupancy(new Vector3i(-2, 3, -1)),
            new BlockOccupancy(new Vector3i(-2, 3, 0)),
            new BlockOccupancy(new Vector3i(-2, 3, 1)),
            new BlockOccupancy(new Vector3i(-2, 3, 2)),
			//Fifth Row
			new BlockOccupancy(new Vector3i(0, 4, -2)),
            new BlockOccupancy(new Vector3i(0, 4, -1)),
            new BlockOccupancy(new Vector3i(0, 4, 0)),
            new BlockOccupancy(new Vector3i(0, 4, 1)),
            new BlockOccupancy(new Vector3i(0, 4, 2)),
            new BlockOccupancy(new Vector3i(1, 4, -2)),
            new BlockOccupancy(new Vector3i(1, 4, -1)),
            new BlockOccupancy(new Vector3i(1, 4, 0)),
            new BlockOccupancy(new Vector3i(1, 4, 1)),
            new BlockOccupancy(new Vector3i(1, 4, 2)),
            new BlockOccupancy(new Vector3i(2, 4, -2)),
            new BlockOccupancy(new Vector3i(2, 4, -1)),
            new BlockOccupancy(new Vector3i(2, 4, 0)),
            new BlockOccupancy(new Vector3i(2, 4, 1)),
            new BlockOccupancy(new Vector3i(2, 4, 2)),
            new BlockOccupancy(new Vector3i(-1, 4, -2)),
            new BlockOccupancy(new Vector3i(-1, 4, -1)),
            new BlockOccupancy(new Vector3i(-1, 4, 0)),
            new BlockOccupancy(new Vector3i(-1, 4, 1)),
            new BlockOccupancy(new Vector3i(-1, 4, 2)),
            new BlockOccupancy(new Vector3i(-2, 4, -2)),
            new BlockOccupancy(new Vector3i(-2, 4, -1)),
            new BlockOccupancy(new Vector3i(-2, 4, 0)),
            new BlockOccupancy(new Vector3i(-2, 4, 1)),
            new BlockOccupancy(new Vector3i(-2, 4, 2)),
			//Sixth Row
			new BlockOccupancy(new Vector3i(0, 5, -2)),
            new BlockOccupancy(new Vector3i(0, 5, -1)),
            new BlockOccupancy(new Vector3i(0, 5, 0)),
            new BlockOccupancy(new Vector3i(0, 5, 1)),
            new BlockOccupancy(new Vector3i(0, 5, 2)),
            new BlockOccupancy(new Vector3i(1, 5, -2)),
            new BlockOccupancy(new Vector3i(1, 5, -1)),
            new BlockOccupancy(new Vector3i(1, 5, 0)),
            new BlockOccupancy(new Vector3i(1, 5, 1)),
            new BlockOccupancy(new Vector3i(1, 5, 2)),
            new BlockOccupancy(new Vector3i(2, 5, -2)),
            new BlockOccupancy(new Vector3i(2, 5, -1)),
            new BlockOccupancy(new Vector3i(2, 5, 0)),
            new BlockOccupancy(new Vector3i(2, 5, 1)),
            new BlockOccupancy(new Vector3i(2, 5, 2)),
            new BlockOccupancy(new Vector3i(-1, 5, -2)),
            new BlockOccupancy(new Vector3i(-1, 5, -1)),
            new BlockOccupancy(new Vector3i(-1, 5, 0)),
            new BlockOccupancy(new Vector3i(-1, 5, 1)),
            new BlockOccupancy(new Vector3i(-1, 5, 2)),
            new BlockOccupancy(new Vector3i(-2, 5, -2)),
            new BlockOccupancy(new Vector3i(-2, 5, -1)),
            new BlockOccupancy(new Vector3i(-2, 5, 0)),
            new BlockOccupancy(new Vector3i(-2, 5, 1)),
            new BlockOccupancy(new Vector3i(-2, 5, 2)),
			//Seventh Row
			new BlockOccupancy(new Vector3i(0, 6, -2)),
            new BlockOccupancy(new Vector3i(0, 6, -1)),
            new BlockOccupancy(new Vector3i(0, 6, 0)),
            new BlockOccupancy(new Vector3i(0, 6, 1)),
            new BlockOccupancy(new Vector3i(0, 6, 2)),
            new BlockOccupancy(new Vector3i(1, 6, -2)),
            new BlockOccupancy(new Vector3i(1, 6, -1)),
            new BlockOccupancy(new Vector3i(1, 6, 0)),
            new BlockOccupancy(new Vector3i(1, 6, 1)),
            new BlockOccupancy(new Vector3i(1, 6, 2)),
            new BlockOccupancy(new Vector3i(2, 6, -2)),
            new BlockOccupancy(new Vector3i(2, 6, -1)),
            new BlockOccupancy(new Vector3i(2, 6, 0)),
            new BlockOccupancy(new Vector3i(2, 6, 1)),
            new BlockOccupancy(new Vector3i(2, 6, 2)),
            new BlockOccupancy(new Vector3i(-1, 6, -2)),
            new BlockOccupancy(new Vector3i(-1, 6, -1)),
            new BlockOccupancy(new Vector3i(-1, 6, 0)),
            new BlockOccupancy(new Vector3i(-1, 6, 1)),
            new BlockOccupancy(new Vector3i(-1, 6, 2)),
            new BlockOccupancy(new Vector3i(-2, 6, -2)),
            new BlockOccupancy(new Vector3i(-2, 6, -1)),
            new BlockOccupancy(new Vector3i(-2, 6, 0)),
            new BlockOccupancy(new Vector3i(-2, 6, 1)),
            new BlockOccupancy(new Vector3i(-2, 6, 2)),
			//Eigth Row
			new BlockOccupancy(new Vector3i(0, 7, -2)),
            new BlockOccupancy(new Vector3i(0, 7, -1)),
            new BlockOccupancy(new Vector3i(0, 7, 0)),
            new BlockOccupancy(new Vector3i(0, 7, 1)),
            new BlockOccupancy(new Vector3i(0, 7, 2)),
            new BlockOccupancy(new Vector3i(1, 7, -2)),
            new BlockOccupancy(new Vector3i(1, 7, -1)),
            new BlockOccupancy(new Vector3i(1, 7, 0)),
            new BlockOccupancy(new Vector3i(1, 7, 1)),
            new BlockOccupancy(new Vector3i(1, 7, 2)),
            new BlockOccupancy(new Vector3i(2, 7, -2)),
            new BlockOccupancy(new Vector3i(2, 7, -1)),
            new BlockOccupancy(new Vector3i(2, 7, 0)),
            new BlockOccupancy(new Vector3i(2, 7, 1)),
            new BlockOccupancy(new Vector3i(2, 7, 2)),
            new BlockOccupancy(new Vector3i(-1, 7, -2)),
            new BlockOccupancy(new Vector3i(-1, 7, -1)),
            new BlockOccupancy(new Vector3i(-1, 7, 0)),
            new BlockOccupancy(new Vector3i(-1, 7, 1)),
            new BlockOccupancy(new Vector3i(-1, 7, 2)),
            new BlockOccupancy(new Vector3i(-2, 7, -2)),
            new BlockOccupancy(new Vector3i(-2, 7, -1)),
            new BlockOccupancy(new Vector3i(-2, 7, 0)),
            new BlockOccupancy(new Vector3i(-2, 7, 1)),
            new BlockOccupancy(new Vector3i(-2, 7, 2)),
			//Ninth Row
			new BlockOccupancy(new Vector3i(0, 8, -2)),
            new BlockOccupancy(new Vector3i(0, 8, -1)),
            new BlockOccupancy(new Vector3i(0, 8, 0)),
            new BlockOccupancy(new Vector3i(0, 8, 1)),
            new BlockOccupancy(new Vector3i(0, 8, 2)),
            new BlockOccupancy(new Vector3i(1, 8, -2)),
            new BlockOccupancy(new Vector3i(1, 8, -1)),
            new BlockOccupancy(new Vector3i(1, 8, 0)),
            new BlockOccupancy(new Vector3i(1, 8, 1)),
            new BlockOccupancy(new Vector3i(1, 8, 2)),
            new BlockOccupancy(new Vector3i(2, 8, -2)),
            new BlockOccupancy(new Vector3i(2, 8, -1)),
            new BlockOccupancy(new Vector3i(2, 8, 0)),
            new BlockOccupancy(new Vector3i(2, 8, 1)),
            new BlockOccupancy(new Vector3i(2, 8, 2)),
            new BlockOccupancy(new Vector3i(-1, 8, -2)),
            new BlockOccupancy(new Vector3i(-1, 8, -1)),
            new BlockOccupancy(new Vector3i(-1, 8, 0)),
            new BlockOccupancy(new Vector3i(-1, 8, 1)),
            new BlockOccupancy(new Vector3i(-1, 8, 2)),
            new BlockOccupancy(new Vector3i(-2, 8, -2)),
            new BlockOccupancy(new Vector3i(-2, 8, -1)),
            new BlockOccupancy(new Vector3i(-2, 8, 0)),
            new BlockOccupancy(new Vector3i(-2, 8, 1)),
            new BlockOccupancy(new Vector3i(-2, 8, 2)),
			//Tenth Row
			new BlockOccupancy(new Vector3i(0, 9, -2)),
            new BlockOccupancy(new Vector3i(0, 9, -1)),
            new BlockOccupancy(new Vector3i(0, 9, 0)),
            new BlockOccupancy(new Vector3i(0, 9, 1)),
            new BlockOccupancy(new Vector3i(0, 9, 2)),
            new BlockOccupancy(new Vector3i(1, 9, -2)),
            new BlockOccupancy(new Vector3i(1, 9, -1)),
            new BlockOccupancy(new Vector3i(1, 9, 0)),
            new BlockOccupancy(new Vector3i(1, 9, 1)),
            new BlockOccupancy(new Vector3i(1, 9, 2)),
            new BlockOccupancy(new Vector3i(2, 9, -2)),
            new BlockOccupancy(new Vector3i(2, 9, -1)),
            new BlockOccupancy(new Vector3i(2, 9, 0)),
            new BlockOccupancy(new Vector3i(2, 9, 1)),
            new BlockOccupancy(new Vector3i(2, 9, 2)),
            new BlockOccupancy(new Vector3i(-1, 9, -2)),
            new BlockOccupancy(new Vector3i(-1, 9, -1)),
            new BlockOccupancy(new Vector3i(-1, 9, 0)),
            new BlockOccupancy(new Vector3i(-1, 9, 1)),
            new BlockOccupancy(new Vector3i(-1, 9, 2)),
            new BlockOccupancy(new Vector3i(-2, 9, -2)),
            new BlockOccupancy(new Vector3i(-2, 9, -1)),
            new BlockOccupancy(new Vector3i(-2, 9, 0)),
            new BlockOccupancy(new Vector3i(-2, 9, 1)),
            new BlockOccupancy(new Vector3i(-2, 9, 2)),
			//Eleventh Row
			new BlockOccupancy(new Vector3i(0, 10, -2)),
            new BlockOccupancy(new Vector3i(0, 10, -1)),
            new BlockOccupancy(new Vector3i(0, 10, 0)),
            new BlockOccupancy(new Vector3i(0, 10, 1)),
            new BlockOccupancy(new Vector3i(0, 10, 2)),
            new BlockOccupancy(new Vector3i(1, 10, -2)),
            new BlockOccupancy(new Vector3i(1, 10, -1)),
            new BlockOccupancy(new Vector3i(1, 10, 0)),
            new BlockOccupancy(new Vector3i(1, 10, 1)),
            new BlockOccupancy(new Vector3i(1, 10, 2)),
            new BlockOccupancy(new Vector3i(2, 10, -2)),
            new BlockOccupancy(new Vector3i(2, 10, -1)),
            new BlockOccupancy(new Vector3i(2, 10, 0)),
            new BlockOccupancy(new Vector3i(2, 10, 1)),
            new BlockOccupancy(new Vector3i(2, 10, 2)),
            new BlockOccupancy(new Vector3i(-1, 10, -2)),
            new BlockOccupancy(new Vector3i(-1, 10, -1)),
            new BlockOccupancy(new Vector3i(-1, 10, 0)),
            new BlockOccupancy(new Vector3i(-1, 10, 1)),
            new BlockOccupancy(new Vector3i(-1, 10, 2)),
            new BlockOccupancy(new Vector3i(-2, 10, -2)),
            new BlockOccupancy(new Vector3i(-2, 10, -1)),
            new BlockOccupancy(new Vector3i(-2, 10, 0)),
            new BlockOccupancy(new Vector3i(-2, 10, 1)),
            new BlockOccupancy(new Vector3i(-2, 10, 2)),
            });
        }

        /// <summary>Hook for mods to customize WorldObject before initialization. You can change housing values here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize WorldObject after initialization.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [MaxStackSize(50)]
    [LocDisplayName("Water Tower")]
    [Ecopedia("Crafted Objects", "Specialty", createAsSubPage: true)]
    [LiquidProducer(typeof(WaterItem), 1.8f)]
    public partial class WaterTowerItem : WorldObjectItem<WaterTowerObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Pumps water from the ground into a pipe network. Produces alot of water");
        [Tooltip(7)] private LocString PowerConsumptionTooltip => Localizer.Do($"Consumes: {Text.Info(200)}w of {new MechanicalPower().Name} power");
    }

    [RequiresSkill(typeof(CarpentrySkill), 3)]
    public partial class WaterTowerRecipe : RecipeFamily
    {
        public WaterTowerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "WaterTower",
                Localizer.DoStr("Water Tower"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronPipeItem), 12, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    new IngredientElement(typeof(MechanicalWaterPumpItem), 1, true),
                    new IngredientElement("Lumber", 20, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<WaterTowerItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;
            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(CarpentrySkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(WaterTowerRecipe), 8, typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent), typeof(CarpentryParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Water Tower"), typeof(WaterTowerRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}

