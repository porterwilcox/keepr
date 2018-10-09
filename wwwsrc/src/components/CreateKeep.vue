<template>
    <v-dialog max-width="50vw" v-model="createKeep" transition="slide-y-transition">
        <v-btn slot="activator" flat fab>
            <v-icon>fas fa-pencil-alt</v-icon>
        </v-btn>
        <v-card>
            <v-toolbar card color="amber darken-4">
                <v-toolbar-title>Create a Keep</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn @click="createKeep = false" fab flat>
                    <v-icon>close</v-icon>
                </v-btn>
            </v-toolbar>
            <form @submit.prevent="submitKeep" class="ma-3">
                <v-text-field @input="validate()" v-model="newKeep.name" color="amber darken-4" single-line clearable placeholder="title" counter="50" maxlength="50"></v-text-field>
                <v-text-field @input="validate()" v-model="newKeep.description" color="amber darken-4" clearable placeholder="text-content" counter="250" maxlength="250"></v-text-field>
                <v-layout id="w">
                    <v-checkbox slot="activator" v-model="newKeep.isPrivate" label="Private" color="amber darken-4"></v-checkbox>
                    <v-tooltip right>
                        <v-icon class="mt-4" slot="activator" small>far fa-question-circle</v-icon>
                        <span>Private keeps can only be viewed by other Keepr users.<br>Public keeps can be viewed by those who're just browsing Keepr.</span>
                    </v-tooltip>
                </v-layout>
                <v-checkbox @change="validate()" v-model="hasImage" label="Include Image" color="amber darken-4"></v-checkbox>
                <v-text-field @input="validate()" v-model="newKeep.img" v-if="hasImage" type="url" color="amber darken-4" clearable placeholder="image url"></v-text-field>
                <v-layout justify-center>
                    <v-btn v-if="readyToSubmit" type="submit" color="amber darken-4">submit</v-btn>
                </v-layout>
            </form>
        </v-card>
    </v-dialog>
</template>
<script>
export default {
    name: 'createKeep',
    data() {
        return {
            createKeep: false,
            hasImage: false,
            newKeep: {
                name: "",
                description: "",
                isPrivate: false,
                img: ""
            },
            readyToSubmit: false
        }
    },
    computed: {
        user() {
            return this.$store.state.user
        }
    },
    methods: {
        validate() {
            if (!this.newKeep.name.length || !this.newKeep.description.length) {
                return
            }
            if (this.hasImage && !this.newKeep.img.length) {
                this.readyToSubmit = false
                return
            }
            this.readyToSubmit = true
        },
        submitKeep() {
            let k = this.newKeep
            if (!k.isPrivate) {
                k.isPrivate = 0
            }
            else
            {
                k.isPrivate = 1
            }
            k.userId = this.user.id
            this.$store.dispatch("postKeep", k)
            this.newKeep = {
                name: "",
                description: "",
                isPrivate: false,
                img: ""
            }
            this.readyToSubmit = false
            this.hasImage = false
            this.createKeep = false
        }
    }
}
</script>
<style>
#w {
    max-width: 100px;
}
</style>
