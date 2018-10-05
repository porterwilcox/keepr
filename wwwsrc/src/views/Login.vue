<template>
    <v-content id="h">
        <v-container fill-height>
            <v-layout justify-center align-center >
                <v-flex sm8 md4>
                    <div class="login">
                        <v-card v-if="loginForm" >
                            <v-toolbar card color="primary">
                                <v-toolbar-title>
                                    <v-icon color="white" large>fab fa-korvue</v-icon>
                                </v-toolbar-title>
                                 <v-spacer></v-spacer>
                                <v-btn @click="loginForm = !loginForm">
                                    Create new account
                                </v-btn>
                            </v-toolbar>
                            <form @submit.prevent="loginUser">
                                <input type="email" v-model="creds.email" placeholder="email">
                                <input type="password" v-model="creds.password" placeholder="password">
                                <button type="submit">Login</button>
                            </form>
                        </v-card>
                        <v-card v-else>
                            <v-toolbar card>
                                <v-toolbar-title>
                                    <v-icon color="primary" large>fab fa-korvue</v-icon>
                                </v-toolbar-title>
                                <v-spacer></v-spacer>
                                <v-btn color="primary" @click="loginForm = !loginForm">
                                    I have an account
                                </v-btn>
                            </v-toolbar>
                            <form @submit.prevent="register">
                                <input type="text" v-model="newUser.username" placeholder="name">
                                <input type="email" v-model="newUser.email" placeholder="email">
                                <input type="password" v-model="newUser.password" placeholder="password">
                                <button type="submit">Create Account</button>
                            </form>
                        </v-card>
                    </div>
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>

<script>
    export default {
        name: "login",
        mounted() {
            //checks for valid session
            this.$store.dispatch("authenticate");
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                }
            };
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        }
    };
</script>
<style>
#h {
    height: 100vh;
}
</style>
