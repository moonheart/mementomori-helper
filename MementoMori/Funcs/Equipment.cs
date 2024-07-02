using MementoMori.Common.Localization;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori;

public partial class MementoMoriFuncs
{
    public async Task AutoEquipmentTraning()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var totalCount = 0;
            while (!token.IsCancellationRequested)
            {
                // await UserGetUserData();
                var equipment = UserSyncData.UserEquipmentDtoInfos.First(d => d.Guid == TrainingEquipmentGuid);
                var equipmentMb = Masters.EquipmentTable.GetById(equipment.EquipmentId);
                var currentParameter = equipment.GetAdditionalParameter(EquipmentTrainingTargetType);
                var m = $"{Masters.TextResourceTable.Get("[CommonForgedLabel]")} {totalCount}, {Masters.TextResourceTable.Get(EquipmentTrainingTargetType)} {currentParameter} ({(double)currentParameter/equipmentMb.AdditionalParameterTotal:P})";
                log(m);
                var targetValue = equipmentMb.AdditionalParameterTotal * EquipmentTrainingTargetPercent;
                switch (EquipmentTrainingTargetType)
                {
                    case BaseParameterType.Health when equipment.AdditionalParameterHealth >= targetValue:
                        return;
                    case BaseParameterType.Energy when equipment.AdditionalParameterEnergy >= targetValue:
                        return;
                    case BaseParameterType.Intelligence when equipment.AdditionalParameterIntelligence >= targetValue:
                        return;
                    case BaseParameterType.Muscle when equipment.AdditionalParameterMuscle >= targetValue:
                        return;
                }

                var response = await GetResponse<TrainingRequest, TrainingResponse>(new TrainingRequest() {EquipmentGuid = TrainingEquipmentGuid, ParameterLockedList = new List<BaseParameterType>()});
                totalCount++;
                await Task.Delay(GameConfig.AutoRequestDelay, token);
            }
        });
    }

    public async Task ReinforcementEquipmentOneTime()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var equipmentDtoInfo = UserSyncData.UserEquipmentDtoInfos.OrderBy(d => d.ReinforcementLv)
                .FirstOrDefault(d => !string.IsNullOrEmpty(d.CharacterGuid) && Masters.EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv);
            if (equipmentDtoInfo != null)
            {
                var response = await GetResponse<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest() {EquipmentGuid = equipmentDtoInfo.Guid, NumberOfTimes = 1});
                log($"{Masters.TextResourceTable.Get("[CommonReinforceLabel]")} {ResourceStrings.Finished}");
            }
        });
    }
}