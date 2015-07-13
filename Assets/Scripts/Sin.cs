using UnityEngine;
using System.Collections;

public class Sin {
	public enum Type{
		Wrath, //Ira
		Pride, //Orgulho
		Envy, //Inveja
		Greed, //Avareza ou ganância
		Gluttony, //Gula
		Lust, //Luxuria
		Sloth //Preguica
	}


	private Type sinType;
	private int amount;

	public Sin(Type sinType, int amount){
		this.sinType = sinType;
		this.amount = amount;
	}

	public Type SinType {
		get {
			return this.sinType;
		}
	}

	public int Amount {
		get {
			return this.amount;
		}

	}

	public int ReduceSinAmount(int amountToReduce){
		if (this.amount - amountToReduce < 0) {
			int toReturn = amount;
			this.amount = 0;
			return toReturn;
		} else {
			int toReturn = amountToReduce;
			amount -= amountToReduce;
			return toReturn;
		}
	}

}
