using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;

namespace MementoMori.Funcs;

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
                var equipmentMb = EquipmentTable.GetById(equipment.EquipmentId);
                var currentParameter = equipment.GetAdditionalParameter(EquipmentTrainingTargetType);
                var m =
                    $"{TextResourceTable.Get("[CommonForgedLabel]")} {totalCount}, {TextResourceTable.Get(EquipmentTrainingTargetType)} {currentParameter} ({(double) currentParameter / equipmentMb.AdditionalParameterTotal:P})";
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

                var response = await GetResponse<TrainingRequest, TrainingResponse>(new TrainingRequest {EquipmentGuid = TrainingEquipmentGuid, ParameterLockedList = new List<BaseParameterType>()});
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
                .FirstOrDefault(d => !string.IsNullOrEmpty(d.CharacterGuid) && EquipmentTable.GetById(d.EquipmentId).EquipmentLv > d.ReinforcementLv);
            if (equipmentDtoInfo != null)
            {
                var response = await GetResponse<ReinforcementRequest, ReinforcementResponse>(new ReinforcementRequest {EquipmentGuid = equipmentDtoInfo.Guid, NumberOfTimes = 1});
                log($"{TextResourceTable.Get("[CommonReinforceLabel]")} {ResourceStrings.Finished}");
            }
        });
    }
}