using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MasterInstaller : MonoInstaller
{
    [NotNull]
    public ThreadedCoroutine singleton_ThreadedCoroutine;
    [NotNull]
    public DungeonManager singleton_DungeonManager;
    [NotNull]
    public ValueAnimator singleton_ValueAnimator;
    [NotNull]
    public AudioPlayer singleton_AudioPlayer;

    public override void InstallBindings()
    {
        // Singletons
        Container.Bind<ThreadedCoroutine>().FromInstance(singleton_ThreadedCoroutine);
        Container.Bind<DungeonManager>().FromInstance(singleton_DungeonManager);
        Container.Bind<ValueAnimator>().FromInstance(singleton_ValueAnimator);
        Container.Bind<AudioPlayer>().FromInstance(singleton_AudioPlayer);

        // Self References
        Container.Bind<Rigidbody2D>().FromComponentSibling();
        Container.Bind<Damageable>().FromComponentSibling();
        Container.Bind<PlayerStats>().FromComponentSibling();
    }

}
