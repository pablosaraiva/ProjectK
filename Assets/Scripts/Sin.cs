using UnityEngine;
using System.Collections;

public class Sin {
	public enum Type{
		Wrath,
		Pride,
		Envy,
		Greed,
		Gluttony,
		Lust,
		Sloth
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

}
