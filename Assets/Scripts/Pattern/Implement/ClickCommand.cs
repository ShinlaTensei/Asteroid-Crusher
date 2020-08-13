using System;
using Pattern.Interface;
using UnityEngine;
using Constant;
// ReSharper disable RedundantJumpStatement

namespace Pattern.Implement
{
    public class ClickGoTo : ICommand
    {
        private GameObject origin;
        public ClickGoTo(GameObject from)
        {
            origin = from;
        }
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            origin.SetActive(false);
            GameObject to = parameter as GameObject;
            if (to != null)
            {
               to.SetActive(true); 
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
    

    public class ClickBuyShip : ICommand
    {
        private Ship ship;
        private event Action<bool> OnResultBuyShip;
        public ClickBuyShip(Ship item, Action<bool> callbackBuyShip)
        {
            ship = item;
            OnResultBuyShip += callbackBuyShip;
        }

        ~ClickBuyShip()
        {
            if (OnResultBuyShip?.GetInvocationList() is Action<bool>[] invocationList)
            {
                foreach (var function in invocationList)
                {
                    OnResultBuyShip -= function;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return false;
        }
        /// <summary>
        /// Handle the buy ship action
        /// </summary>
        /// <param name="parameter"> price of the item pass as integer </param>
        public void Execute(object parameter)
        {
            int itemPrice = (int) parameter;
            // ReSharper disable once RedundantJumpStatement
            if (itemPrice > PlayerManager.Instance.UserData.money)
            {
                GameManager.Instance.ShowMessage(Message.NotEnoughMoney);
                OnResultBuyShip?.Invoke(false);
                return;
            }
            PlayerManager.Instance.BuyItem(itemPrice);
            ship.shipInfo.isOwn = true;
            //PlayerManager.Instance.UserData.ownShip.Add(ship);
            OnResultBuyShip?.Invoke(true);
        }
    }

    public class ClickChoseShip : ICommand
    {
        private Ship ship;
        private Action chooseCallback;

        public ClickChoseShip(Ship item, Action callback)
        {
            ship = item;
            chooseCallback = callback;
        }

        public bool CanExecute(object parameter = null)
        {
            if (!ship.shipInfo.isOwn)
            {
                GameManager.Instance.ShowMessage(Message.DontOwn);
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            PlayerManager.Instance.choosingShip = ship.gameObject;
            GameManager.Instance.ShowMessage("Play now?", () =>
            {
                GameManager.Instance.gameStateMachine.Initialize(new GameBeginState());
            });
            chooseCallback?.Invoke();
        }
    }
    /// <summary>
    /// Handle the upgrade ship action, execute with 0 arguments
    /// </summary>
    public class ClickUpgradeShip : ICommand
    {
        private TypeUpgrade type;
        private Ship ship;

        public ClickUpgradeShip(TypeUpgrade t, ref Ship item)
        {
            type = t;
            ship = item;
        }
        public bool CanExecute(object parameter = null)
        {
            if (!ship.shipInfo.isOwn)
            {
                GameManager.Instance.ShowMessage(Message.DontOwn);
                return false;
            }

            return true;
        }

        public void Execute(object parameter = null)
        {
            switch (type)
            {
                case TypeUpgrade.Guns:
                    ship.shipInfo.numberOfCannon += 1;
                    break;
                case TypeUpgrade.Speed:
                    ship.shipInfo.speed += 15;
                    break;
                case TypeUpgrade.FuelConsumption:
                    ship.shipInfo.fuelConsumption += 15;
                    break;
                case TypeUpgrade.Endurance:
                    ship.shipInfo.endurance += 15;
                    break;
                default:
                    break;
            }
            
            PlayerManager.Instance.BuyItem(Const.PRICE_UPGRADE);
        }
    }
}