using Flapp_BLL.Interfaces;
using System;

namespace Flapp_BLL.Managers {
    public class ConnectionManager {
        private IConnectionRepo _repo;

        public ConnectionManager(IConnectionRepo repo) {
            _repo = repo;
        }
        public bool IsServerConnected() {
            try { return _repo.IsServerConnected(); }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
    }
}
