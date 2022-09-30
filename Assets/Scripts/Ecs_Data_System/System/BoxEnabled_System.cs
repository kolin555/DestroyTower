using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Transforms;

public partial class BoxEnabled_System : SystemBase
{
    public float height = -6f;
    public float timeCount = 0;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        height = -6f;
        timeCount = 0;
    }

    protected override void OnUpdate()
    {
        timeCount += Time.DeltaTime;
        timeCount = (float)math.clamp(timeCount,0, 0.1);
        if (timeCount == 0.1f)
        {
            timeCount = 0;
            height = height + 1 ;
        }
        var enableHeight = height;
        var beginCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();

        var ecb = beginCommandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.WithAll<Disabled>().ForEach((Entity en, int entityInQueryIndex,in Translation translation, in Box_Data data) =>
        {
            if (translation.Value.y < enableHeight)
            {
                ecb.RemoveComponent<Disabled>(entityInQueryIndex, en);
            }
            

        }).ScheduleParallel();
        beginCommandBuffer.AddJobHandleForProducer(Dependency);
    }

   
}
