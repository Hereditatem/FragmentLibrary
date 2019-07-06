import React from 'react';
import { Header, Segment, Form, Button } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';
import {
    Configuration,
    FragmentApiFactory
} from '../../api'
class TileAdd extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',

        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.frontScanFile = React.createRef();
        this.backScanFile = React.createRef();
        this.frontScanFileWithoutBg = React.createRef();
        this.backScanFileWithoutBg = React.createRef();
    }
    
    handleChange(event) {
        const {
            name,
            value
        } = event.target;
        this.setState({...this.state, [name]: value});
    }

    handleSubmit(){
        var config = new Configuration();
        var factory = FragmentApiFactory(config).fragmentIndex(
            this.state.name,
            this.frontScanFile.current.files[0],
            this.backScanFile.current.files[0],
            this.frontScanFileWithoutBg.current.files[0],
            this.backScanFileWithoutBg.current.files[0]
        ).then((res, error) =>{
            if(res && !error){
                const {
                    id
                } = res;
                this.props.history.push(`/tile/${id}/alignment`);
            }
        }
        );
        
    }

    render() {
        return (
            <div className="tile-add">
                <Header as="h1">
                    Adicionar nova Digitalização
                </Header>
                <Form onSubmit={this.handleSubmit}>
                    <Form.Field width={3}>
                        <label>Nome</label>
                        <input
                            onChange={this.handleChange}
                            type="text"
                            name="name"
                            required />
                    </Form.Field>
                    <Segment>
                        <Header as="h4">
                            Originais
                </Header>
                        <Form.Group>
                            <Form.Field>
                                <label>Frente</label>
                                <input 
                                    type="file"
                                    name="frontScanFile" 
                                    required
                                    ref={this.frontScanFile}/>
                            </Form.Field>
                            <Form.Field>
                                <label>Trás</label>
                                <input
                                    type="file"
                                    name="backScanFile"
                                    required
                                    ref={this.backScanFile}/>
                            </Form.Field>
                        </Form.Group>
                    </Segment>
                    <Segment>
                        <Header as="h4">
                            Com fundo transparente
                        </Header>
                        <Form.Group>
                            <Form.Field>
                                <label>Frente</label>
                                <input
                                    type="file"
                                    name="frontScanFileWithoutBg"
                                    required
                                    ref={this.frontScanFileWithoutBg}/>
                            </Form.Field>
                            <Form.Field>
                                <label>Trás</label>
                                <input 
                                    type="file"
                                    name="backScanFileWithoutBg"
                                    required
                                    ref={this.backScanFileWithoutBg} />
                            </Form.Field>
                        </Form.Group>
                    </Segment>
                    <Button type="submit">Submeter</Button>
                </Form>
            </div>
        );
    }
}

export default withRouter(TileAdd);