using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal class Controller2
	{
		public static void readMessage(Message msg)
		{
			try
			{
				Res.outz("cmd=" + msg.command);
				switch (msg.command)
				{
				case sbyte.MinValue:
					readInfoEffChar(msg);
					break;
				case sbyte.MaxValue:
					readInfoRada(msg);
					break;
				case 113:
				{
					int loop = msg.reader().readByte();
					int layer = msg.reader().readByte();
					int id = msg.reader().readUnsignedByte();
					short x = msg.reader().readShort();
					short y = msg.reader().readShort();
					short loopCount = msg.reader().readShort();
					EffecMn.addEff(new Effect(id, x, y, layer, loop, loopCount));
					break;
				}
				case 48:
				{
					sbyte b26 = msg.reader().readByte();
					ServerListScreen.ipSelect = b26;
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					break;
				}
				case 31:
				{
					int num25 = msg.reader().readInt();
					sbyte b20 = msg.reader().readByte();
					if (b20 == 1)
					{
						short smallID = msg.reader().readShort();
						sbyte b21 = -1;
						int[] array5 = null;
						short wimg = 0;
						short himg = 0;
						try
						{
							b21 = msg.reader().readByte();
							if (b21 > 0)
							{
								sbyte b22 = msg.reader().readByte();
								array5 = new int[b22];
								for (int num26 = 0; num26 < b22; num26++)
								{
									array5[num26] = msg.reader().readByte();
								}
								wimg = msg.reader().readShort();
								himg = msg.reader().readShort();
							}
						}
						catch (Exception)
						{
						}
						if (num25 == Char.myCharz().charID)
						{
							Char.myCharz().petFollow = new PetFollow();
							Char.myCharz().petFollow.smallID = smallID;
							if (b21 > 0)
							{
								Char.myCharz().petFollow.SetImg(b21, array5, wimg, himg);
							}
							break;
						}
						Char char2 = GameScr.findCharInMap(num25);
						char2.petFollow = new PetFollow();
						char2.petFollow.smallID = smallID;
						if (b21 > 0)
						{
							char2.petFollow.SetImg(b21, array5, wimg, himg);
						}
					}
					else if (num25 == Char.myCharz().charID)
					{
						Char.myCharz().petFollow.remove();
						Char.myCharz().petFollow = null;
					}
					else
					{
						Char char3 = GameScr.findCharInMap(num25);
						char3.petFollow.remove();
						char3.petFollow = null;
					}
					break;
				}
				case -89:
					GameCanvas.open3Hour = ((msg.reader().readByte() == 1) ? true : false);
					break;
				case 42:
				{
					GameCanvas.endDlg();
					LoginScr.isContinueToLogin = false;
					Char.isLoadingMap = false;
					sbyte haveName = msg.reader().readByte();
					if (GameCanvas.registerScr == null)
					{
						GameCanvas.registerScr = new RegisterScreen(haveName);
					}
					GameCanvas.registerScr.switchToMe();
					break;
				}
				case 52:
				{
					sbyte b29 = msg.reader().readByte();
					if (b29 == 1)
					{
						int num40 = msg.reader().readInt();
						if (num40 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(m: true);
							Char.myCharz().cx = msg.reader().readShort();
							Char.myCharz().cy = msg.reader().readShort();
						}
						else
						{
							Char char4 = GameScr.findCharInMap(num40);
							if (char4 != null)
							{
								char4.setMabuHold(m: true);
								char4.cx = msg.reader().readShort();
								char4.cy = msg.reader().readShort();
							}
						}
					}
					if (b29 == 0)
					{
						int num41 = msg.reader().readInt();
						if (num41 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(m: false);
						}
						else
						{
							GameScr.findCharInMap(num41)?.setMabuHold(m: false);
						}
					}
					if (b29 == 2)
					{
						int charId2 = msg.reader().readInt();
						int id4 = msg.reader().readInt();
						Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId2);
						mabu2.eat(id4);
					}
					if (b29 == 3)
					{
						GameScr.mabuPercent = msg.reader().readByte();
					}
					break;
				}
				case 51:
				{
					int charId = msg.reader().readInt();
					Mabu mabu = (Mabu)GameScr.findCharInMap(charId);
					sbyte id2 = msg.reader().readByte();
					short x2 = msg.reader().readShort();
					short y2 = msg.reader().readShort();
					sbyte b23 = msg.reader().readByte();
					Char[] array6 = new Char[b23];
					int[] array7 = new int[b23];
					for (int num28 = 0; num28 < b23; num28++)
					{
						int num29 = msg.reader().readInt();
						Res.outz("char ID=" + num29);
						array6[num28] = null;
						if (num29 != Char.myCharz().charID)
						{
							array6[num28] = GameScr.findCharInMap(num29);
						}
						else
						{
							array6[num28] = Char.myCharz();
						}
						array7[num28] = msg.reader().readInt();
					}
					mabu.setSkill(id2, x2, y2, array6, array7);
					break;
				}
				case -127:
					readLuckyRound(msg);
					break;
				case -126:
				{
					sbyte b10 = msg.reader().readByte();
					Res.outz("type quay= " + b10);
					if (b10 == 1)
					{
						sbyte b11 = msg.reader().readByte();
						string num11 = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num11, finish);
					}
					if (b10 == 0)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					break;
				}
				case -122:
				{
					short id3 = msg.reader().readShort();
					Npc npc = GameScr.findNPCInMap(id3);
					sbyte b28 = msg.reader().readByte();
					npc.duahau = new int[b28];
					Res.outz("N DUA HAU= " + b28);
					for (int num39 = 0; num39 < b28; num39++)
					{
						npc.duahau[num39] = msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					break;
				}
				case 102:
				{
					sbyte b17 = msg.reader().readByte();
					if (b17 == 0 || b17 == 1 || b17 == 2 || b17 == 6)
					{
						BigBoss2 bigBoss = Mob.getBigBoss2();
						if (bigBoss == null)
						{
							break;
						}
						if (b17 == 6)
						{
							bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
							break;
						}
						sbyte b18 = msg.reader().readByte();
						Char[] array = new Char[b18];
						int[] array2 = new int[b18];
						for (int num18 = 0; num18 < b18; num18++)
						{
							int num19 = msg.reader().readInt();
							array[num18] = null;
							if (num19 != Char.myCharz().charID)
							{
								array[num18] = GameScr.findCharInMap(num19);
							}
							else
							{
								array[num18] = Char.myCharz();
							}
							array2[num18] = msg.reader().readInt();
						}
						bigBoss.setAttack(array, array2, b17);
					}
					if (b17 == 3 || b17 == 4 || b17 == 5 || b17 == 7)
					{
						BachTuoc bachTuoc = Mob.getBachTuoc();
						if (bachTuoc == null)
						{
							break;
						}
						if (b17 == 7)
						{
							bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
							break;
						}
						if (b17 == 3 || b17 == 4)
						{
							sbyte b19 = msg.reader().readByte();
							Char[] array3 = new Char[b19];
							int[] array4 = new int[b19];
							for (int num20 = 0; num20 < b19; num20++)
							{
								int num21 = msg.reader().readInt();
								array3[num20] = null;
								if (num21 != Char.myCharz().charID)
								{
									array3[num20] = GameScr.findCharInMap(num21);
								}
								else
								{
									array3[num20] = Char.myCharz();
								}
								array4[num20] = msg.reader().readInt();
							}
							bachTuoc.setAttack(array3, array4, b17);
						}
						if (b17 == 5)
						{
							short xMoveTo = msg.reader().readShort();
							bachTuoc.move(xMoveTo);
						}
					}
					if (b17 > 9 && b17 < 30)
					{
						readActionBoss(msg, b17);
					}
					break;
				}
				case 101:
				{
					Res.outz("big boss--------------------------------------------------");
					BigBoss bigBoss2 = Mob.getBigBoss();
					if (bigBoss2 == null)
					{
						break;
					}
					sbyte b24 = msg.reader().readByte();
					if (b24 == 0 || b24 == 1 || b24 == 2 || b24 == 4 || b24 == 3)
					{
						if (b24 == 3)
						{
							bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
							bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
							bigBoss2.setFly();
						}
						else
						{
							sbyte b25 = msg.reader().readByte();
							Res.outz("CHUONG nChar= " + b25);
							Char[] array8 = new Char[b25];
							int[] array9 = new int[b25];
							for (int num30 = 0; num30 < b25; num30++)
							{
								int num31 = msg.reader().readInt();
								Res.outz("char ID=" + num31);
								array8[num30] = null;
								if (num31 != Char.myCharz().charID)
								{
									array8[num30] = GameScr.findCharInMap(num31);
								}
								else
								{
									array8[num30] = Char.myCharz();
								}
								array9[num30] = msg.reader().readInt();
							}
							bigBoss2.setAttack(array8, array9, b24);
						}
					}
					if (b24 == 5)
					{
						bigBoss2.haftBody = true;
						bigBoss2.status = 2;
					}
					if (b24 == 6)
					{
						bigBoss2.getDataB2();
						bigBoss2.x = msg.reader().readShort();
						bigBoss2.y = msg.reader().readShort();
					}
					if (b24 == 7)
					{
						bigBoss2.setAttack(null, null, b24);
					}
					if (b24 == 8)
					{
						bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
						bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
						bigBoss2.status = 2;
					}
					if (b24 == 9)
					{
						bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
					}
					break;
				}
				case -120:
				{
					long num27 = mSystem.currentTimeMillis();
					Service.logController = num27 - Service.curCheckController;
					Service.gI().sendCheckController();
					break;
				}
				case -121:
				{
					long num22 = mSystem.currentTimeMillis();
					Service.logMap = num22 - Service.curCheckMap;
					Service.gI().sendCheckMap();
					break;
				}
				case 100:
				{
					sbyte b31 = msg.reader().readByte();
					sbyte b32 = msg.reader().readByte();
					Item item2 = null;
					if (b31 == 0)
					{
						item2 = Char.myCharz().arrItemBody[b32];
					}
					if (b31 == 1)
					{
						item2 = Char.myCharz().arrItemBag[b32];
					}
					short num43 = msg.reader().readShort();
					if (num43 == -1)
					{
						break;
					}
					item2.template = ItemTemplates.get(num43);
					item2.quantity = msg.reader().readInt();
					item2.info = msg.reader().readUTF();
					item2.content = msg.reader().readUTF();
					sbyte b33 = msg.reader().readByte();
					if (b33 == 0)
					{
						break;
					}
					item2.itemOption = new ItemOption[b33];
					for (int num44 = 0; num44 < item2.itemOption.Length; num44++)
					{
						int num45 = msg.reader().readUnsignedByte();
						Res.outz("id o= " + num45);
						int param3 = msg.reader().readUnsignedShort();
						if (num45 != -1)
						{
							item2.itemOption[num44] = new ItemOption(num45, param3);
						}
					}
					break;
				}
				case -123:
				{
					int charId3 = msg.reader().readInt();
					if (GameScr.findCharInMap(charId3) != null)
					{
						GameScr.findCharInMap(charId3).perCentMp = msg.reader().readByte();
					}
					break;
				}
				case -119:
					Char.myCharz().rank = msg.reader().readInt();
					break;
				case -117:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if (GameScr.gI().percentMabu == 100)
					{
						GameScr.gI().mabuEff = true;
					}
					if (GameScr.gI().percentMabu == 101)
					{
						Npc.mabuEff = true;
					}
					break;
				case -116:
					GameScr.canAutoPlay = ((msg.reader().readByte() == 1) ? true : false);
					break;
				case -115:
					Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					break;
				case -113:
				{
					sbyte[] array10 = new sbyte[5];
					for (int num32 = 0; num32 < 5; num32++)
					{
						array10[num32] = msg.reader().readByte();
						Res.outz("vlue i= " + array10[num32]);
					}
					GameScr.gI().onKSkill(array10);
					GameScr.gI().onOSkill(array10);
					GameScr.gI().onCSkill(array10);
					break;
				}
				case -111:
				{
					short num23 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int num24 = 0; num24 < num23; num24++)
					{
						string iD = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(iD, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					break;
				}
				case 125:
				{
					sbyte fusion = msg.reader().readByte();
					int num10 = msg.reader().readInt();
					if (num10 == Char.myCharz().charID)
					{
						Char.myCharz().setFusion(fusion);
					}
					else if (GameScr.findCharInMap(num10) != null)
					{
						GameScr.findCharInMap(num10).setFusion(fusion);
					}
					break;
				}
				case 124:
				{
					short num12 = msg.reader().readShort();
					string text3 = msg.reader().readUTF();
					Res.outz("noi chuyen = " + text3 + "npc ID= " + num12);
					GameScr.findNPCInMap(num12)?.addInfo(text3);
					break;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num42 = msg.reader().readInt();
					short xPos = msg.reader().readShort();
					short yPos = msg.reader().readShort();
					sbyte b30 = msg.reader().readByte();
					Char char5 = null;
					if (num42 == Char.myCharz().charID)
					{
						char5 = Char.myCharz();
					}
					else if (GameScr.findCharInMap(num42) != null)
					{
						char5 = GameScr.findCharInMap(num42);
					}
					if (char5 != null)
					{
						ServerEffect.addServerEffect((b30 != 0) ? 173 : 60, char5, 1);
						char5.setPos(xPos, yPos, b30);
					}
					break;
				}
				case 122:
				{
					short num33 = msg.reader().readShort();
					Res.outz("second login = " + num33);
					LoginScr.timeLogin = num33;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					break;
				}
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					break;
				case -124:
				{
					sbyte b6 = msg.reader().readByte();
					sbyte b7 = msg.reader().readByte();
					if (b7 == 0)
					{
						if (b6 == 2)
						{
							int num4 = msg.reader().readInt();
							if (num4 == Char.myCharz().charID)
							{
								Char.myCharz().removeEffect();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeEffect();
							}
						}
						int num5 = msg.reader().readUnsignedByte();
						int num6 = msg.reader().readInt();
						if (num5 == 32)
						{
							if (b6 == 1)
							{
								int num7 = msg.reader().readInt();
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().holdEffID = num5;
									GameScr.findCharInMap(num7).setHoldChar(Char.myCharz());
								}
								else if (GameScr.findCharInMap(num6) != null && num7 != Char.myCharz().charID)
								{
									GameScr.findCharInMap(num6).holdEffID = num5;
									GameScr.findCharInMap(num7).setHoldChar(GameScr.findCharInMap(num6));
								}
								else if (GameScr.findCharInMap(num6) != null && num7 == Char.myCharz().charID)
								{
									GameScr.findCharInMap(num6).holdEffID = num5;
									Char.myCharz().setHoldChar(GameScr.findCharInMap(num6));
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeHoleEff();
							}
						}
						if (num5 == 33)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().protectEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).protectEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeProtectEff();
							}
						}
						if (num5 == 39)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().huytSao = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).huytSao = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeHuytSao();
							}
						}
						if (num5 == 40)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().blindEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).blindEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeBlindEff();
							}
						}
						if (num5 == 41)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().sleepEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).sleepEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeSleepEff();
							}
						}
						if (num5 == 42)
						{
							if (b6 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().stone = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().stone = false;
							}
						}
					}
					if (b7 != 1)
					{
						break;
					}
					int num8 = msg.reader().readUnsignedByte();
					sbyte b8 = msg.reader().readByte();
					Res.outz("modbHoldID= " + b8 + " skillID= " + num8 + "eff ID= " + b6);
					if (num8 == 32)
					{
						if (b6 == 1)
						{
							int num9 = msg.reader().readInt();
							if (num9 == Char.myCharz().charID)
							{
								GameScr.findMobInMap(b8).holdEffID = num8;
								Char.myCharz().setHoldMob(GameScr.findMobInMap(b8));
							}
							else if (GameScr.findCharInMap(num9) != null)
							{
								GameScr.findMobInMap(b8).holdEffID = num8;
								GameScr.findCharInMap(num9).setHoldMob(GameScr.findMobInMap(b8));
							}
						}
						else
						{
							GameScr.findMobInMap(b8).removeHoldEff();
						}
					}
					if (num8 == 40)
					{
						if (b6 == 1)
						{
							GameScr.findMobInMap(b8).blindEff = true;
						}
						else
						{
							GameScr.findMobInMap(b8).removeBlindEff();
						}
					}
					if (num8 == 41)
					{
						if (b6 == 1)
						{
							GameScr.findMobInMap(b8).sleepEff = true;
						}
						else
						{
							GameScr.findMobInMap(b8).removeSleepEff();
						}
					}
					break;
				}
				case -125:
				{
					ChatTextField.gI().isShow = false;
					string text = msg.reader().readUTF();
					Res.outz("titile= " + text);
					sbyte b4 = msg.reader().readByte();
					ClientInput.gI().setInput(b4, text);
					for (int k = 0; k < b4; k++)
					{
						ClientInput.gI().tf[k].name = msg.reader().readUTF();
						sbyte b5 = msg.reader().readByte();
						if (b5 == 0)
						{
							ClientInput.gI().tf[k].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						if (b5 == 1)
						{
							ClientInput.gI().tf[k].setIputType(TField.INPUT_TYPE_ANY);
						}
						if (b5 == 2)
						{
							ClientInput.gI().tf[k].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					break;
				}
				case -110:
				{
					sbyte b27 = msg.reader().readByte();
					if (b27 == 1)
					{
						int num36 = msg.reader().readInt();
						sbyte[] array11 = Rms.loadRMS(num36 + string.Empty);
						if (array11 == null)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, num36, array11);
						}
					}
					if (b27 == 0)
					{
						int num37 = msg.reader().readInt();
						short num38 = msg.reader().readShort();
						sbyte[] data = new sbyte[num38];
						msg.reader().read(ref data, 0, num38);
						Rms.saveRMS(num37 + string.Empty, data);
					}
					break;
				}
				case 93:
				{
					string str = msg.reader().readUTF();
					str = Res.changeString(str);
					GameScr.gI().chatVip(str);
					break;
				}
				case -106:
				{
					short num34 = msg.reader().readShort();
					int num35 = msg.reader().readShort();
					if (ItemTime.isExistItem(num34))
					{
						ItemTime.getItemById(num34).initTime(num35);
						break;
					}
					ItemTime o = new ItemTime(num34, num35);
					Char.vItemTime.addElement(o);
					break;
				}
				case -105:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					break;
				case -103:
				{
					sbyte b12 = msg.reader().readByte();
					if (b12 == 0)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b13 = msg.reader().readByte();
						for (int l = 0; l < b13; l++)
						{
							Item item = new Item();
							short num13 = msg.reader().readShort();
							if (num13 != -1)
							{
								item.template = ItemTemplates.get(num13);
								sbyte b14 = msg.reader().readByte();
								if (b14 != -1)
								{
									item.itemOption = new ItemOption[b14];
									for (int m = 0; m < item.itemOption.Length; m++)
									{
										int num14 = msg.reader().readUnsignedByte();
										int param2 = msg.reader().readUnsignedShort();
										if (num14 != -1)
										{
											item.itemOption[m] = new ItemOption(num14, param2);
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else if (b12 == 1)
					{
						int num15 = msg.reader().readInt();
						sbyte b15 = msg.reader().readByte();
						Res.outz("---------------actionFlag1:  " + num15 + " : " + b15);
						if (num15 == Char.myCharz().charID)
						{
							Char.myCharz().cFlag = b15;
						}
						else if (GameScr.findCharInMap(num15) != null)
						{
							GameScr.findCharInMap(num15).cFlag = b15;
						}
						GameScr.gI().getFlagImage(num15, b15);
					}
					else
					{
						if (b12 != 2)
						{
							break;
						}
						sbyte b16 = msg.reader().readByte();
						int num16 = msg.reader().readShort();
						PKFlag pKFlag = new PKFlag();
						pKFlag.cflag = b16;
						pKFlag.IDimageFlag = num16;
						GameScr.vFlag.addElement(pKFlag);
						for (int n = 0; n < GameScr.vFlag.size(); n++)
						{
							PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(n);
							Res.outz("i: " + n + "  cflag: " + pKFlag2.cflag + "   IDimageFlag: " + pKFlag2.IDimageFlag);
						}
						for (int num17 = 0; num17 < GameScr.vCharInMap.size(); num17++)
						{
							Char @char = (Char)GameScr.vCharInMap.elementAt(num17);
							if (@char != null && @char.cFlag == b16)
							{
								@char.flagImage = num16;
							}
						}
						if (Char.myCharz().cFlag == b16)
						{
							Char.myCharz().flagImage = num16;
						}
					}
					break;
				}
				case -102:
				{
					sbyte b9 = msg.reader().readByte();
					if (b9 != 0 && b9 == 1)
					{
						GameCanvas.loginScr.isLogin2 = false;
						Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
						LoginScr.isLoggingIn = true;
					}
					break;
				}
				case -101:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text2 = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text2);
					Service.gI().setClientType();
					Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
					break;
				}
				case -100:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag = true;
					}
					sbyte b = msg.reader().readByte();
					Res.outz("t Indxe= " + b);
					GameCanvas.panel.maxPageShop[b] = msg.reader().readByte();
					GameCanvas.panel.currPageShop[b] = msg.reader().readByte();
					Res.outz("max page= " + GameCanvas.panel.maxPageShop[b] + " curr page= " + GameCanvas.panel.currPageShop[b]);
					int num = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop[b] = new Item[num];
					for (int i = 0; i < num; i++)
					{
						short num2 = msg.reader().readShort();
						if (num2 == -1)
						{
							continue;
						}
						Res.outz("template id= " + num2);
						Char.myCharz().arrItemShop[b][i] = new Item();
						Char.myCharz().arrItemShop[b][i].template = ItemTemplates.get(num2);
						Char.myCharz().arrItemShop[b][i].itemId = msg.reader().readShort();
						Char.myCharz().arrItemShop[b][i].buyCoin = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].buyGold = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].buyType = msg.reader().readByte();
						Char.myCharz().arrItemShop[b][i].quantity = msg.reader().readByte();
						Char.myCharz().arrItemShop[b][i].isMe = msg.reader().readByte();
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
						sbyte b2 = msg.reader().readByte();
						if (b2 != -1)
						{
							Char.myCharz().arrItemShop[b][i].itemOption = new ItemOption[b2];
							for (int j = 0; j < Char.myCharz().arrItemShop[b][i].itemOption.Length; j++)
							{
								int num3 = msg.reader().readUnsignedByte();
								int param = msg.reader().readUnsignedShort();
								if (num3 != -1)
								{
									Char.myCharz().arrItemShop[b][i].itemOption[j] = new ItemOption(num3, param);
									Char.myCharz().arrItemShop[b][i].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[b][i]);
								}
							}
						}
						sbyte b3 = msg.reader().readByte();
						if (b3 == 1)
						{
							int headTemp = msg.reader().readShort();
							int bodyTemp = msg.reader().readShort();
							int legTemp = msg.reader().readShort();
							int bagTemp = msg.reader().readShort();
							Char.myCharz().arrItemShop[b][i].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
						}
					}
					if (flag)
					{
						GameCanvas.panel2.setTabKiGui();
					}
					GameCanvas.panel.setTabShop();
					GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
					break;
				}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[b2];
					for (int i = 0; i < b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, price, idTicket);
				}
				else if (b == 1)
				{
					sbyte b4 = msg.reader().readByte();
					short[] array2 = new short[b4];
					for (int j = 0; j < b4; j++)
					{
						array2[j] = msg.reader().readShort();
					}
					CrackBallScr.gI().DoneCrackBallScr(array2);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = msg.reader().readShort();
						int no = i + 1;
						int idIcon = msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						Char charInfo = null;
						sbyte b2 = msg.reader().readByte();
						if (b2 == 0)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							int head = msg.reader().readShort();
							int body = msg.reader().readShort();
							int leg = msg.reader().readShort();
							int bag = msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						ItemOption[] array = null;
						if (b4 != 0)
						{
							array = new ItemOption[b4];
							for (int j = 0; j < array.Length; j++)
							{
								int num3 = msg.reader().readUnsignedByte();
								int param = msg.reader().readUnsignedShort();
								sbyte activeCard = msg.reader().readByte();
								if (num3 != -1)
								{
									array[j] = new ItemOption(num3, param);
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b2, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b3);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						if (b3 > 0)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, num);
					RadarScr.gI().switchToMe();
				}
				else if (b == 1)
				{
					int id2 = msg.reader().readShort();
					sbyte use2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id2) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id2).SetUse(use2);
					}
					RadarScr.SetListUse();
				}
				else if (b == 2)
				{
					int num4 = msg.reader().readShort();
					sbyte level = msg.reader().readByte();
					int num5 = 0;
					for (int k = 0; k < RadarScr.list.size(); k++)
					{
						Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
						if (info_RadaScr2 != null)
						{
							if (info_RadaScr2.id == num4)
							{
								info_RadaScr2.SetLevel(level);
							}
							if (info_RadaScr2.level > 0)
							{
								num5++;
							}
						}
					}
					RadarScr.SetNum(num5, RadarScr.list.size());
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
					}
				}
				else if (b == 3)
				{
					int id3 = msg.reader().readShort();
					sbyte amount2 = msg.reader().readByte();
					sbyte max_amount2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id3).SetAmount(amount2, max_amount2);
					}
					if (Info_RadaScr.GetInfo(RadarScr.listUse, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, id3).SetAmount(amount2, max_amount2);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (b == 0)
				{
					int id = msg.reader().readShort();
					int layer = msg.reader().readByte();
					int loop = msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					@char?.addEffChar(new Effect(id, @char, layer, loop, loopCount, isStand));
				}
				else if (b == 1)
				{
					int id2 = msg.reader().readShort();
					@char?.removeEffChar(0, id2);
				}
				else if (b == 2)
				{
					@char?.removeEffChar(-1, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				sbyte idBoss = msg.reader().readByte();
				NewBoss newBoss = Mob.getNewBoss(idBoss);
				if (newBoss == null)
				{
					return;
				}
				if (actionBoss == 10)
				{
					short xMoveTo = msg.reader().readShort();
					short yMoveTo = msg.reader().readShort();
					newBoss.move(xMoveTo, yMoveTo);
				}
				if (actionBoss >= 11 && actionBoss <= 20)
				{
					sbyte b = msg.reader().readByte();
					Char[] array = new Char[b];
					int[] array2 = new int[b];
					for (int i = 0; i < b; i++)
					{
						int num = msg.reader().readInt();
						array[i] = null;
						if (num != Char.myCharz().charID)
						{
							array[i] = GameScr.findCharInMap(num);
						}
						else
						{
							array[i] = Char.myCharz();
						}
						array2[i] = msg.reader().readInt();
					}
					sbyte dir = msg.reader().readByte();
					newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
				}
				if (actionBoss == 21)
				{
					newBoss.xTo = msg.reader().readShort();
					newBoss.yTo = msg.reader().readShort();
					newBoss.setFly();
				}
				if (actionBoss == 22)
				{
				}
				if (actionBoss == 23)
				{
					newBoss.setDie();
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
