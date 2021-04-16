import Loadable from 'react-loadable';
import Loading from '../Loading/index';

const LoadableComponent = (component) =>
    Loadable({
        loader: component,
        loading: Loading,
    });

export default LoadableComponent;
