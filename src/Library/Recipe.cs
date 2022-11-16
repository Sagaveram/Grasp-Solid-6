//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        
        public bool Cooked{get;private set;}=false;
        
        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        //Retorna la suma del tiempo de todos los pasos
        // Agregado por Expert
        public int GetCookTime()
        {
            int tiempoTotal=0;
            foreach (BaseStep step in this.steps)
            {
                tiempoTotal=tiempoTotal+step.Time;
    
            }
            return tiempoTotal;

        }
        //Expert, método que ayuda a pasar cook a True ya que esta proprertie es de solo lectura
        public void CookedTrue()
        {
            Cooked=true;
        }
        
        //Se utiliza el patrón Adapter para modificar la clase
        //Se encuentra este método en Recipie por Expert
        public void Cook()
        {
            int tiempoCoccion=GetCookTime();
            TimerAdapter timerAdapter=new TimerAdapter();
            timerAdapter.recipe=this;
            CountdownTimer countdownTimer=new CountdownTimer();
            countdownTimer.Register(tiempoCoccion,timerAdapter);
            timerAdapter.TimeOut();
                
        }

        //Agregar una propiedad bool Cooked de sólo lectura; 
        //es false al inicio y pasa a true cuando se invoca void Cook() y pasa el tiempo indicado por GetCookTime()
        //Agregar un método void Cook(). Usando la clase CountdownTimer provista,
        // debe pasar la propiedad Cooked a true cuando pase el tiempo indicado por GetCookTime()
        
    }
}