import React from 'react';
import { Route, Switch } from 'react-router';
import Utils from '../../utils'
const Router = () => {
    const UserLayout = Utils.getRoute('/user').component;
    const MainLayout = Utils.getRoute('/').component;

    return (
        <Switch>
            <Route path="/user" render={(props) => <UserLayout {...props} />} />
            <Route path="/" render={(props) => <MainLayout {...props} exact />} />
        </Switch>
    );
};

export default Router;