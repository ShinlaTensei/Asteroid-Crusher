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

        public ClickChoseShip(Ship item)
        {
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

        public void Execute(object parameter)
        {
            PlayerManager.Instance.choosingShip = ship.gameObject;
        }
    }
    /// <summary>
    /// Handle the upgrade ship action, execute with 0 arguments
    /// </summary>
    public class ClickUpgradeShip : ICommand
    {
        enum TypeUpgrade
        {
            Guns, Speed, FuelConsumption, Endurance
        }

        private TypeUpgrade type;
        private Ship ship;

        public ClickUpgradeShip(int t, Ship item)
        {
            type = (TypeUpgrade) t;
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

        public void Execute(object parameter)
        {
            
        }
    }
}