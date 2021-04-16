import React, { Component } from 'react';
import { Redirect, Route, Switch } from 'react-router';
import About from '../../scenes/About';
import Home from '../../scenes/Home';
import LoginForm from '../../scenes/Login';
import NotFound from '../../scenes/NotFound';
class Router extends Component {
    state = {}
    render() {
        return (
            <Switch>
                <Route path='/login' component={LoginForm} />
                <Route path='/about' component={About} />
                <Route path="/notfound" component={NotFound}></Route>
                <Route exact path='/' component={Home} />
                <Redirect path="/notfound" />
            </Switch>
        );
    }
}

export default Router;