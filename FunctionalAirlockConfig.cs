using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace FunctionalAirlocks
{
	public class FunctionalAirlockConfig : PressureDoorConfig
	{
		public const string Id = "FunctionalAirlock";
		public const string DisplayName = "Functional Airlock";
		public const string Description = "";
		public static string Effect =
			$"Consumes {ELEMENTS.FERTILIZER.NAME}, {ELEMENTS.CARBONDIOXIDE.NAME} and {ELEMENTS.WATER.NAME} " +
			$"to grow {ELEMENTS.ALGAE.NAME} and emit some {ELEMENTS.OXYGEN.NAME}.\n\nRequires {UI.FormatAsLink("Light", "LIGHT")}  to grow.";

		/*
		private static readonly List<Storage.StoredItemModifier> PollutedWaterStorageModifiers = new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal
		};
		*/

		public override BuildingDef CreateBuildingDef()
		{
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
				id: Id,  
				width: 3, 
				height: 1, 
				anim: "door_external_kanim", 
				hitpoints: BUILDINGS.HITPOINTS.TIER1, 
				construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2, 
				construction_mass: TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3,
				construction_materials: MATERIALS.ALL_METALS,
				melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1,
				build_location_rule: BuildLocationRule.Tile, 
				decor: TUNING.BUILDINGS.DECOR.PENALTY.TIER1,
				noise: NOISE_POLLUTION.NOISY.TIER0);
			
			buildingDef.Overheatable = false;
			buildingDef.RequiresPowerInput = true;
			buildingDef.EnergyConsumptionWhenActive = 240f;
			buildingDef.Entombable = false;
			buildingDef.IsFoundation = true;
			buildingDef.ViewMode = OverlayModes.Power.ID;
			buildingDef.TileLayer = ObjectLayer.FoundationTile;
			buildingDef.AudioCategory = "Metal";
			buildingDef.PermittedRotations = PermittedRotations.Unrotatable;
			buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
			buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
			//buildingDef.OutputConduitType = ConduitType.Gas;
			//buildingDef.UtilityOutputOffset = new CellOffset(1, 0);

			//GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, Id);

			SoundEventVolumeCache.instance.AddVolume(
				"door_external_kanim", 
				"Open_DoorPressure", 
				NOISE_POLLUTION.NOISY.TIER2);
			SoundEventVolumeCache.instance.AddVolume(
				"door_external_kanim", 
				"Close_DoorPressure", 
				NOISE_POLLUTION.NOISY.TIER2);
			
			return buildingDef;
		}

		/*
		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			var storage1 = go.AddOrGet<Storage>();
			storage1.showInUI = true;

			var storage2 = go.AddComponent<Storage>();
			storage2.capacityKg = 5f;
			storage2.showInUI = true;
			storage2.SetDefaultStoredItemModifiers(PollutedWaterStorageModifiers);
			storage2.allowItemRemoval = false;
			storage2.storageFilters = new List<Tag> { SimHashes.Algae.CreateTag(), SimHashes.CarbonDioxide.CreateTag() };

			var manualDeliveryKg1 = go.AddOrGet<ManualDeliveryKG>();
			manualDeliveryKg1.SetStorage(storage1);
			manualDeliveryKg1.requestedItemTag = SimHashes.Fertilizer.CreateTag();
			manualDeliveryKg1.capacity = 90f;
			manualDeliveryKg1.refillMass = 18f;
			manualDeliveryKg1.choreTypeIDHash = Db.Get().ChoreTypes.Fetch.IdHash;

			var manualDeliveryKg2 = go.AddComponent<ManualDeliveryKG>();
			manualDeliveryKg2.SetStorage(storage1);
			manualDeliveryKg2.requestedItemTag = SimHashes.Water.CreateTag();
			manualDeliveryKg2.capacity = 360f;
			manualDeliveryKg2.refillMass = 72f;
			manualDeliveryKg2.allowPause = true;
			manualDeliveryKg2.choreTypeIDHash = Db.Get().ChoreTypes.Fetch.IdHash;

			var algaeHabitat = go.AddOrGet<FunctionalAirlock>();
			algaeHabitat.PressureSampleOffset = new CellOffset(0, 1);

			var elementConverter = go.AddComponent<ElementConverter>();
			elementConverter.consumedElements = new[]
			{
				new ElementConverter.ConsumedElement(SimHashes.CarbonDioxide.CreateTag(), 0.01375f),
				new ElementConverter.ConsumedElement(SimHashes.Fertilizer.CreateTag(), 0.000625f),
				new ElementConverter.ConsumedElement(SimHashes.Water.CreateTag(), 0.005625f)
			};
			elementConverter.outputElements = new[]
			{
				new ElementConverter.OutputElement(0.005f, SimHashes.Oxygen, 303.15f, false, false, 0.0f, 1f),
				new ElementConverter.OutputElement(0.015f, SimHashes.Algae, 303.15f, false, true, 0.0f, 1f)
			};

			var elementDropper = go.AddComponent<ElementDropper>();
			elementDropper.emitMass = 5;
			elementDropper.emitTag = SimHashes.Algae.CreateTag();

			var elementConsumer = go.AddOrGet<ElementConsumer>();
			elementConsumer.elementToConsume = SimHashes.CarbonDioxide;
			elementConsumer.consumptionRate = 0.01375f;
			elementConsumer.consumptionRadius = 3;
			elementConsumer.showInStatusPanel = true;
			elementConsumer.storeOnConsume = true;
			elementConsumer.sampleCellOffset = new Vector3(0.0f, 1f, 0.0f);
			elementConsumer.isRequired = true;

			var passiveElementConsumer = go.AddComponent<PassiveElementConsumer>();
			passiveElementConsumer.elementToConsume = SimHashes.Water;
			passiveElementConsumer.consumptionRate = 1.2f;
			passiveElementConsumer.consumptionRadius = 1;
			passiveElementConsumer.showDescriptor = false;
			passiveElementConsumer.storeOnConsume = true;
			passiveElementConsumer.capacityKG = 360f;
			passiveElementConsumer.showInStatusPanel = false;

			go.AddOrGet<AnimTileable>();

			Prioritizable.AddRef(go);
		}
		*/

		public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
		{
			base.DoPostConfigurePreview(def, go);
			go.AddComponent<KAnimControllerResize>().width = 3.0f;
		}

		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			base.DoPostConfigureUnderConstruction(go);
			go.AddComponent<KAnimControllerResize>().width = 3.0f;
		}


		public override void DoPostConfigureComplete(GameObject go)
		{
			base.DoPostConfigureComplete(go);
			go.AddComponent<KAnimControllerResize>().width = 3.0f;

			/*
			// copied from GasPumpConfig.cs
			go.AddOrGet<LogicOperationalController>();
			go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
			go.AddOrGet<LoopingSounds>();
			go.AddOrGet<EnergyConsumer>();
			go.AddOrGet<Pump>();
			go.AddOrGet<Storage>().capacityKg = 1f;

			ElementConsumer elementConsumer = go.AddOrGet<ElementConsumer>();
			elementConsumer.configuration = ElementConsumer.Configuration.AllGas;
			elementConsumer.consumptionRate = 3f;
			elementConsumer.storeOnConsume = true;
			elementConsumer.showInStatusPanel = false;
			elementConsumer.consumptionRadius = (byte)1;
			elementConsumer.sampleCellOffset = new Vector3(-2.0f, 0f, 0.0f);

			ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
			conduitDispenser.conduitType = ConduitType.Gas;
			conduitDispenser.alwaysDispense = false;
			conduitDispenser.elementFilter = (SimHashes[])null;
			go.AddOrGetDef<OperationalController.Def>();
			go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);

			//FunctionalAirlock functionalAirlock = go.AddOrGet<FunctionalAirlock> ();
			*/
		}
	}
}
