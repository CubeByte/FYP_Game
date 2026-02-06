using UnityEngine;

public class EffectInstance 
{
   public Effect effect;
   public int turnsRemaining;
   
   public GameObject currentlyActiveGameObject;
   public ParticleSystem currentTickParticle;

   public EffectInstance(Effect effect)
   {
      this.effect = effect;
      turnsRemaining = effect.durationOfTurns;
   }
}
