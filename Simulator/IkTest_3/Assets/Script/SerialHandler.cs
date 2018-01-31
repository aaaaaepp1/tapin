using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialHandler : MonoBehaviour {

	public string portName = "COM4";
	public int boudRate = 9600;
	public bool write90Deg = false;
	public bool onSendMessage = false;
	public bool closeSerial = false;
	public int baseAngle = 90;
	public int boomAngle = 90;
	public int armAngle = 90;
	public int wristAngle = 90;
	public int fingerAngle = 90;

	private SerialPort mainSerialPort;
	private Thread readThread;
	private Thread writeThread;
	private bool isRunning = false;
	private string receivedMessage;
	private string sendMessage = "90,90,90,90,90;";
	private bool isNewMessageReceived = false;
	private char arduinoSerialWave = '-';

	// Use this for initialization
	void Start () {
		this.Open ();
	}
	
	// Update is called once per frame
	void Update () {

		if (this.isNewMessageReceived) {
			Debug.Log ("-" + receivedMessage);
			this.isNewMessageReceived = false;
		}

		if (this.closeSerial) {
			this.Close ();
			this.closeSerial = false;
		}

		if (this.write90Deg) {
			this.sendMessage = "90,90,90,90,90;";
		} else {
			this.sendMessage = 
				(baseAngle < 0 ? 0 : baseAngle > 180 ? 180 : baseAngle) + "," +
				(boomAngle < 0 ? 0 : boomAngle > 180 ? 180 : boomAngle) + "," +
				(armAngle < 0 ? 0 : armAngle > 180 ? 180 : armAngle) + "," +
				(wristAngle < 0 ? 0 : wristAngle > 180 ? 180 : wristAngle) + "," +
				(fingerAngle < 45 ? 45 : fingerAngle > 135 ? 135 : fingerAngle) + ";";
		}
	}

	void OnDestroy () {
		this.Close ();
	}

	public void SetAngles (int ba, int bo, int ar, int wr, int fi) {
		this.baseAngle = ba;
		this.boomAngle = bo;
		this.armAngle = ar;
		this.wristAngle = wr;
		this.fingerAngle = fi;
	}

	private void Open () {
		try {
			this.mainSerialPort = new SerialPort (this.portName, this.boudRate, Parity.None, 8, StopBits.One);
			this.mainSerialPort.ReadTimeout = 500;
			this.mainSerialPort.WriteTimeout = 500;
			this.mainSerialPort.Open ();

			this.isRunning = true;

			this.readThread = new Thread (Read);
			this.readThread.Start ();

			this.writeThread = new Thread (Write);
			this.writeThread.Start ();

		} catch (System.Exception e) {
			Debug.LogError (e);
		}
	}

	private void Close () {
		Debug.Log (" ===== CLOSE SERIAL CONNECTION ===== ");

		this.isRunning = false;

		if (this.readThread != null && this.readThread.IsAlive) {
			this.readThread.Join ();
		}

		if (this.writeThread != null && this.writeThread.IsAlive) {
			this.writeThread.Join ();
		}

		if (this.mainSerialPort != null && this.mainSerialPort.IsOpen) {
			this.mainSerialPort.Close ();
			this.mainSerialPort.Dispose ();
		}
	}

	private void Read () {
		while (this.isRunning && this.mainSerialPort != null && this.mainSerialPort.IsOpen) {
			Thread.Sleep (2);
			try {

				string cReceivedMessage = this.mainSerialPort.ReadLine ();
				//	char currentWave = cReceivedMessage.ToCharArray(0, 1)[0];
//				if (currentWave != this.arduinoSerialWave) {
				//	this.message = cReceivedMessage;
//					this.isNewMessageReceived = true;
//					this.arduinoSerialWave = currentWave;
//				} else {
//					this.isNewMessageReceived = false;
//				}
				this.receivedMessage = cReceivedMessage;
				this.isNewMessageReceived = true;
				
			} catch (System.Exception e) {
//				Debug.LogError (e);
			}
		}
	}

	public void Write () {
		while (this.isRunning) {
			if (this.onSendMessage) {
				Thread.Sleep (50);
				try {
					this.mainSerialPort.Write (this.sendMessage);
				} catch (System.Exception e) {
					Debug.LogError (e);
				}
			}
		}

	}


}
